using Intro.Core.Entities;
using Intro.Persistence.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Intro.Application.Queries
{
    public class MongoDbAccountQueries : IAccountQueries
    {
        private readonly MongoDbContext _context;

        public MongoDbAccountQueries(MongoDbContext context)
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