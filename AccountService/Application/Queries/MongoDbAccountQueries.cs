using AccountService.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AccountService.Core.Entities;
using MongoDB.Driver;

namespace AccountService.Application.Queries
{
    public class MongoDbAccountQueries : IAccountQueries
    {
        private readonly IDbInfrastructure<IMongoCollection<Account>> _context;

        public MongoDbAccountQueries(IDbInfrastructure<IMongoCollection<Account>> context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll() => Get(_ => true)
            .ToArray();

        public Account GetBy(ulong id) => Get(x => x.Id == id)
            .FirstOrDefault();

        private IEnumerable<Account> Get(Expression<Func<Account, bool>> filter)
        {
            var documents = _context.Accounts;
            var accounts = new List<Account>();
            using (var cursor = documents.FindSync(filter))
                while (cursor.MoveNext())
                    accounts.AddRange(cursor.Current);

            return accounts;
        }
    }
}