using System;
using AccountService.Core.Entities;
using MongoDB.Driver;

namespace AccountService.Persistence.MongoDb
{
    public class MongoDbContext : IDbInfrastructure<IMongoCollection<Account>>
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(string connectionString, string dbName)
        {
            var connString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentException("Connection string is null or empty!")
                : connectionString;
            var client = new MongoClient(connString);

            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<Account> Accounts => _db.GetCollection<Account>("Accounts");
    }
}
