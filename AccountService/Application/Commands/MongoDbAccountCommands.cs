﻿using AccountService.Core.Commands;
using AccountService.Core.Entities;
using AccountService.Core.Exceptions.Account;
using AccountService.Core.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AccountService.Application.Commands
{
    public sealed class MongoDbAccountCommands : ICommands<Account>
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
            _logger.LogInformation($"Updating account with ID {account.Id}");
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
            try
            {
                _logger.LogInformation($"Adding account with ID {account.Id}");
                _context.Accounts
                    .InsertOne(account);
            }
            catch (MongoWriteException ex)
            {
                _logger.LogWarning($"Couldn't insert new Account with ID {account.Id}");
                throw new AccountException("Couldn't update Account", ex);
            }
        }
    }
}