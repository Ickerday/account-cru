using Castle.Core.Internal;
using Intro.Application.Exceptions;
using Intro.Application.Queries;
using Intro.Core.Entities;
using Intro.Persistence;
using System.Collections.Generic;

namespace Intro.Application.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Add(Account account);
        Account GetBy(ulong id);
        void Update(ulong id, Account account);
    }

    public class AccountService : IAccountService
    {
        private readonly AccountingContext _context;
        private readonly IAccountQueries _accountQueries;

        public AccountService(AccountingContext context, IAccountQueries accountQueries)
        {
            _context = context;
            _accountQueries = accountQueries;
        }

        public IEnumerable<Account> GetAccounts() => _accountQueries.GetAll();

        public void Add(Account account)
        {
            if (!IsValid(account))
                throw new AccountDataInvalidException();


        }

        private static bool IsValid(Account account)
        {
            return account.Id > ulong.MinValue
                   && !account.Name.IsNullOrEmpty()
                   && account.AvailableFunds <= account.Balance;
        }

        public void Update(ulong id, Account newAccount)
        {
            var oldAccount = GetBy(id);

            if (oldAccount == null)
                throw new AccountNotFoundException($"No Account with id {id} found");

            MapAccount(newAccount, oldAccount);

            _context.Update(oldAccount);
            _context.SaveChanges();
        }

        private static void MapAccount(Account newAccount, Account oldAccount)
        {
            oldAccount.Name = newAccount.Name;
            oldAccount.AvailableFunds = newAccount.AvailableFunds;
            oldAccount.Balance = newAccount.Balance;
            oldAccount.HasCard = newAccount.HasCard;
        }

        public Account GetBy(ulong id) => _accountQueries.GetBy(id);
    }
}
