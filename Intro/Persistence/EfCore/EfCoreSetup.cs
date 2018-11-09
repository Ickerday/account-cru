using Intro.Application.Commands;
using Intro.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Intro.Persistence.EfCore
{
    public static class EfCoreSetup
    {
        public static void AddEfCore(this IServiceCollection services, string connStr)
        {
            var connectionString = string.IsNullOrWhiteSpace(connStr)
                ? throw new ArgumentException("Connection string invalid!", nameof(connStr))
                : connStr;

            services.AddDbContext<AccountingContext>
                (options => options.UseSqlServer(connectionString));

            services.AddScoped<IAccountCommands, EfCoreAccountCommands>();
            services.AddScoped<IAccountQueries, EfAccountQueries>();
        }
    }
}