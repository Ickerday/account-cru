using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Exceptions.Account;
using AccountService.Domain.Search;
using AccountService.Persistence;
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
            var spec = new AccountIdMatchesSpecification(id);
            var result = FindWith(spec.ToExpression())
                .FirstOrDefault();

            if (result == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            return result;
        }

        public IEnumerable<Account> FindWith(Specification<Account> specification)
        {
            _logger.LogInformation($"Searching for Accounts following a {specification.GetType().FullName}");
            return GetAll().Where(specification.ToExpression()
                .Compile());
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