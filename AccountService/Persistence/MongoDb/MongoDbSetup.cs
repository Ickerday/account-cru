using AccountService.Application.Commands;
using AccountService.Application.Queries;
using AccountService.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AccountService.Persistence.MongoDb
{
    public static class MongoDbSetup
    {
        public static void AddMongoDb(this IServiceCollection services, string connStr, string dbName)
        {
            services.AddTransient<IDbInfrastructure<IMongoCollection<Account>>, MongoDbContext>
                (x => new MongoDbContextProvider(connStr, dbName).GetContext());

            services.AddTransient<IAccountCommands, MongoDbAccountCommands>();
            services.AddTransient<IAccountQueries, MongoDbAccountQueries>();
        }
    }
}