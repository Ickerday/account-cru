using AccountService.Application.Commands;
using AccountService.Application.Queries;
using AccountService.Core.Entities;
using AccountService.Core.Persistence;
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

            AddCqrs(services);
        }

        private static void AddCqrs(IServiceCollection services)
        {
            services.AddScoped<IAccountCommands, MongoDbAccountCommands>();
            services.AddScoped<IAccountQueries, MongoDbAccountQueries>();
        }
    }
}