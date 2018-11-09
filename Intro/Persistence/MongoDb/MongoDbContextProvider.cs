using Intro.Core.Entities;
using MongoDB.Driver;

namespace Intro.Persistence.MongoDb
{
    public class MongoDbContextProvider
    {
        private readonly string _connectionString;
        private readonly string _dbName;

        public MongoDbContextProvider(string connectionString, string dbName)
        {
            _connectionString = connectionString;
            _dbName = dbName;
        }

        public IDbInfrastructure<IMongoCollection<Account>> GetContext() => new MongoDbContext(_connectionString, _dbName);
    }
}