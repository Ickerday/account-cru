using Castle.Core.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Account = Intro.Core.Entities.Account;

namespace Intro.Persistence
{
    public class MongoDbAccountStore
    {
        private readonly IMongoDatabase _db;
        public IEnumerable<Account> Accounts => GetAccounts();

        public MongoDbAccountStore(string connectionString)
        {
            var connString = connectionString.IsNullOrEmpty()
                ? throw new ArgumentException("Connection string is null or empty!")
                : connectionString;

            var client = new MongoClient(connString);
            _db = client.GetDatabase("AccountCRUD");
        }

        private IEnumerable<Account> GetAccounts()
        {
            var documents = _db.GetCollection<Account>("Accounts");

            var accounts = new List<Account>();
            using (var cursor = documents.FindSync(null))
                while (cursor.MoveNext())
                    accounts.AddRange(cursor.Current);

            return accounts;
        }
    }
}
