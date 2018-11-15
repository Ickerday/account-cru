using AccountService.Core.Entities;
using AccountService.Core.Exceptions.Account;
using AccountService.Core.Persistence;
using AccountService.Core.Queries;
using AccountService.Core.Search;
using AccountService.Search;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Application.Queries
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
            var spec = new AccountIdMatchesSpecification(id);
            var result = FindWith(spec)
                .FirstOrDefault();

            if (result == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            return result;
        }

        public IEnumerable<Account> FindWith(Specification<Account> specification)
        {
            _logger.LogInformation($"Searching for Accounts following a {specification.GetType().FullName}");
            var result = new List<Account>();
            using (var cursor = _context.Accounts.FindSync(specification.ToExpression()))
                while (cursor.MoveNext())
                    result.AddRange(cursor.Current);

            return result;
        }

        private IEnumerable<Account> FindWith(Expression<Func<Account, bool>> filter)
        {
            _logger.LogInformation($"Searching for Accounts with a {filter.GetType().FullName}");
            var result = new List<Account>();
            using (var cursor = _context.Accounts.FindSync(filter))
                while (cursor.MoveNext())
                    result.AddRange(cursor.Current);

            return result;
        }
    }
}