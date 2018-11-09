using Intro.Application.Commands;
using Intro.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Intro.Persistence.MongoDb
{
    public static class MongoDbSetup
    {
        public static void AddMongoDb(this IServiceCollection services, string connStr, string dbName)
        {
            services.AddScoped(x => new MongoDbContextProvider(connStr, dbName).GetContext());

            services.AddScoped<IAccountCommands, MongoDbAccountCommands>();
            services.AddScoped<IAccountQueries, MongoDbAccountQueries>();
        }
    }
}