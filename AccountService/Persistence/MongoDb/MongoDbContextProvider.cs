namespace AccountService.Persistence.MongoDb
{
    public sealed class MongoDbContextProvider
    {
        private readonly string _connectionString;
        private readonly string _dbName;

        public MongoDbContextProvider(string connectionString, string dbName)
        {
            _connectionString = connectionString;
            _dbName = dbName;
        }

        public MongoDbContext GetContext() => new MongoDbContext(_connectionString, _dbName);
    }
}
