using Castle.Core.Internal;
using Intro.Application.Commands;
using Intro.Application.Exceptions;
using Intro.Application.Queries;
using Intro.Core.Entities;
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
        private readonly IAccountQueries _queries;
        private readonly IAccountCommands _commands;

        public AccountService(IAccountQueries queries, IAccountCommands commands)
        {
            _queries = queries;
            _commands = commands;
        }

        public IEnumerable<Account> GetAccounts() => _queries.GetAll();

        public void Add(Account account)
        {
            if (!IsValid(account))
                throw new AccountDataInvalidException();

            _commands.Add(account);
        }

        private static bool IsValid(Account account) => account.Id > ulong.MinValue
            && !account.Name.IsNullOrEmpty()
            && account.AvailableFunds <= account.Balance;

        public void Update(ulong id, Account newAccount)
        {
            var oldAccount = GetBy(id);

            if (oldAccount == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            MapAccount(newAccount, oldAccount);

            _commands.Update(oldAccount);
        }

        private static void MapAccount(Account newAccount, Account oldAccount)
        {
            oldAccount.Name = newAccount.Name;
            oldAccount.AvailableFunds = newAccount.AvailableFunds;
            oldAccount.Balance = newAccount.Balance;
            oldAccount.HasCard = newAccount.HasCard;
        }

        public Account GetBy(ulong id) => _queries.GetBy(id);
    }
}
