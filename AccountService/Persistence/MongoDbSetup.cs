using AccountService.Application.Accounts.Commands;
using AccountService.Application.Accounts.Queries;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Persistence.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AccountService.Persistence
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
            services.AddScoped<ICommands<Account>, MongoDbAccountCommands>();
            services.AddScoped<IQueries<Account>, MongoDbAccountQueries>();
        }
    }
}