using Intro.Core.Entities;
using Intro.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intro.Application.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Add(Account account);
        Account Find(ulong id);
        void Update(ulong id, Account account);
    }

    public class AccountService : IAccountService
    {
        private readonly AccountingContext _context;

        public AccountService(AccountingContext context) =>
            _context = context;

        public IEnumerable<Account> GetAccounts() => _context.Accounts
                .ToArray();

        public void Add(Account account)
        {
            _context.Accounts
              .Add(account);

            _context.SaveChanges();
        }

        public void Update(ulong id, Account newAccount)
        {
            var oldAccount = Find(id);

            if (oldAccount == null)
                throw new ArgumentNullException(@"No Account with id {id} found");

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

        public Account Find(ulong id) => _context.Accounts
            .Find(id);
    }
}
