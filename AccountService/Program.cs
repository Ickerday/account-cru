using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.IO;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AccountService
{
    public class Program
    {
        private static readonly Logger Logger = NLogBuilder.ConfigureNLog("nlog.config")
            .GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            try
            {
                Logger.Debug("Starting Main()");
                CreateWebHostBuilder(args).Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
