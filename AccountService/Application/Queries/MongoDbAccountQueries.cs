using AccountService.Core.Entities;
using AccountService.Core.Exceptions;
using AccountService.Core.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Application.Queries
{
    public class MongoDbAccountQueries : IAccountQueries
    {
        private readonly IDbInfrastructure<IMongoCollection<Account>> _context;
        private readonly ILogger<MongoDbAccountQueries> _logger;

        public MongoDbAccountQueries(IDbInfrastructure<IMongoCollection<Account>> context,
            ILogger<MongoDbAccountQueries> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Account> GetAll()
        {
            _logger.LogInformation("Getting all Accounts");
            return Get(_ => true)
                .ToArray();
        }

        public Account GetBy(ulong id)
        {
            _logger.LogInformation($"Searching for Account with ID {id}");
            var result = Get(x => x.Id == id)
                .FirstOrDefault();

            if (result == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            return result;
        }

        private IEnumerable<Account> Get(Expression<Func<Account, bool>> filter)
        {
            var documents = _context.Accounts;
            var result = new List<Account>();
            using (var cursor = documents.FindSync(filter))
                while (cursor.MoveNext())
                    result.AddRange(cursor.Current);

            return result;
        }
    }
}