using AccountService.Application.Interfaces;
using AccountService.Domain;
using AccountService.Domain.Entities;
using AccountService.Domain.Exceptions.Account;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Application.Accounts.Queries
{
    public class MongoDbAccountQueries : IQueries<Account>
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
            return FindWith(_ => true);
        }

        public Account GetBy(ulong id)
        {
            _logger.LogInformation($"Searching for Account with ID {id}");
            var result = FindWith(x => x.Id == id)
                .FirstOrDefault();

            if (result == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            return result;
        }

        public IEnumerable<Account> FindWith(ISpecificationBuilder<Account> builder)
        {
            _logger.LogInformation($"Searching for Accounts with {builder.GetType().Name}");
            var compiledSpec = builder.Build().Compile();

            return _context.Accounts
                .AsQueryable()
                .Where(compiledSpec);
        }

        private IEnumerable<Account> FindWith(Expression<Func<Account, bool>> filter)
        {
            _logger.LogInformation($"Searching for Accounts with {filter.GetType().Name}");
            var result = new List<Account>();
            using (var cursor = _context.Accounts.FindSync(filter))
                while (cursor.MoveNext())
                    result.AddRange(cursor.Current);

            return result;
        }
    }
}