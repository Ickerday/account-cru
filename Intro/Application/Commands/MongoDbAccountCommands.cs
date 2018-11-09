using Intro.Application.Exceptions;
using Intro.Core.Entities;
using Intro.Persistence.MongoDb;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Intro.Application.Commands
{
    public class MongoDbAccountCommands : IAccountCommands
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<MongoDbAccountCommands> _logger;

        public MongoDbAccountCommands(MongoDbContext context, ILogger<MongoDbAccountCommands> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Update(Account account)
        {
            _logger.LogInformation($"Updating account with ID {account.Id} in {_context.GetType().FullName}");
            var result = _context.Accounts
                .ReplaceOne(x => x.Id == account.Id, account)
                .ModifiedCount;

            if (result == 1L)
                return;

            _logger.LogWarning($"No account with ID {account.Id} found or data was invalid");
            throw new AccountException("Couldn't update Account");
        }

        public void Add(Account account)
        {
            _logger.LogInformation($"Adding account with ID {account.Id} to {_context.GetType().FullName}");
            _context.Accounts
                .InsertOne(account);
        }
    }
}