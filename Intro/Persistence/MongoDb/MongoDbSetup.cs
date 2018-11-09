using Intro.Application.Commands;
using Intro.Application.Queries;
using Intro.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Intro.Persistence.MongoDb
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