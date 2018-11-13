using AccountService.Core.Entities;
using AccountService.Core.Exceptions;
using AccountService.Persistence.EfCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Commands
{
    public class EfCoreAccountCommands : IAccountCommands
    {
        private readonly AccountingContext _context;
        private readonly ILogger<EfCoreAccountCommands> _logger;

        public EfCoreAccountCommands(AccountingContext context, ILogger<EfCoreAccountCommands> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Account account)
        {
            if (!AccountHelpers.IsValid(account))
                throw new AccountDataInvalidException("Invalid new Account data");

            _context.Accounts
                .Add(account);

            _context.SaveChanges();
            _logger.LogInformation($"Created Account with ID {account.Id}");
        }

        public void Update(ulong id, Account newAccount)
        {
            var oldAccount = _context.Accounts.Find(id);
            if (oldAccount == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            AccountHelpers.MapAccount(newAccount, oldAccount);

            _logger.LogInformation($"Updating account with ID {id}");
            _context.Update(oldAccount);
            _context.SaveChanges();
        }
    }
}