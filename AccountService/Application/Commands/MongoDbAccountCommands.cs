using AccountService.Core.Entities;
using AccountService.Core.Exceptions;
using AccountService.Core.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AccountService.Application.Commands
{
    public sealed class MongoDbAccountCommands : IAccountCommands
    {
        private readonly IDbInfrastructure<IMongoCollection<Account>> _context;
        private readonly ILogger<MongoDbAccountCommands> _logger;

        public MongoDbAccountCommands(IDbInfrastructure<IMongoCollection<Account>> context,
            ILogger<MongoDbAccountCommands> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Update(ulong id, Account account)
        {
            _logger.LogInformation($"Updating account with ID {id}");
            var result = _context.Accounts
                .ReplaceOne(x => x.Id == account.Id, account);

            if (result.MatchedCount != 1L)
            {
                var notMatchedMessage = $"No account with ID {id} found";
                _logger.LogWarning(notMatchedMessage);
                throw new AccountNotFoundException(notMatchedMessage);
            }

            if (result.ModifiedCount == 1L)
                return;

            var notModifiedMessage = $"Account with ID {id} not updated";
            _logger.LogWarning(notModifiedMessage);
            throw new AccountException(notModifiedMessage);
        }

        public void Add(Account account)
        {
            try
            {
                _logger.LogInformation($"Adding account with ID {account.Id}");
                _context.Accounts
                    .InsertOne(account);
            }
            catch (MongoWriteException ex)
            {
                _logger.LogError($"Couldn't insert new Account with ID {account.Id}");
                throw new AccountException("Couldn't update Account", ex);
            }
        }
    }
}