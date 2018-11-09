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
            _context.Accounts
                .ReplaceOne(x => x.Id == account.Id, account);
        }

        public void Add(Account account)
        {
            _logger.LogInformation($"Adding account with ID {account.Id} to {_context.GetType().FullName}");
            _context.Accounts
                .InsertOne(account);
        }
    }
}