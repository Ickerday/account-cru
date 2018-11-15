using AccountService.Persistence.EfCore;
using AccountService.Persistence.Exceptions;
using AccountService.Persistence.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence
{
    internal enum DatabaseEngineEnum
    {
        EfCore,
        MongoDb
    }

    public static class DatabaseInitializer
    {
        public static void Initalize(IConfiguration configuration, IServiceCollection services)
        {
            var databaseEngine = configuration["databaseEngine"];
            switch (databaseEngine)
            {
                case (nameof(DatabaseEngineEnum.EfCore)):
                    var mongoConfig = configuration.GetSection("MongoDb");
                    services.AddMongoDb(mongoConfig["ConnectionString"], mongoConfig["Database"]);
                    break;
                case (nameof(DatabaseEngineEnum.MongoDb)):
                    services.AddEfCore(configuration.GetConnectionString("EfCore"));
                    break;
                default:
                    throw new InvalidPersistenceConfigurationException("Invalid databaseEngine string. Check your appsettings.json");
            }
        }
    }
}
