using System;
using AccountService.Domain;
using AccountService.Domain.Entities;
using MongoDB.Driver;

namespace AccountService.Persistence.MongoDb
{
    public sealed class MongoDbContext : IDbInfrastructure<IMongoCollection<Account>>
    {
        private readonly IMongoDatabase _db;

        public IMongoCollection<Account> Accounts => _db.GetCollection<Account>("Accounts");

        public MongoDbContext(string connectionString, string dbName)
        {
            var connString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentException("Connection string is null or empty!")
                : connectionString;
            var client = new MongoClient(connString);

            _db = client.GetDatabase(dbName);
        }
    }
}
