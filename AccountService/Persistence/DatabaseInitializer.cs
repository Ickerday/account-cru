using AccountService.Domain.Exceptions.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence
{
    internal class DatabaseEngine
    {
        internal const string EfCore = "efcore";
        internal const string MongoDb = "mongodb";
    }

    public static class DatabaseInitializer
    {
        public static void Initalize(IConfiguration configuration, IServiceCollection services)
        {
            switch (configuration["databaseEngine"])
            {
                case (DatabaseEngine.MongoDb):
                    var mongoConfig = configuration.GetSection("MongoDb");
                    services.AddMongoDb(mongoConfig["ConnectionString"], mongoConfig["Database"]);
                    break;
                case (DatabaseEngine.EfCore):
                    services.AddEfCore(configuration.GetConnectionString("EfCore"));
                    break;
                default:
                    throw new InvalidPersistenceConfigurationException("Invalid databaseEngine string. Check your appsettings.json");
            }
        }
    }
}
