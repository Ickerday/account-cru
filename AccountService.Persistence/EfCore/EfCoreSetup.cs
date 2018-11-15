using System;
using AccountService.Application.Commands;
using AccountService.Application.Queries;
using AccountService.Core.Commands;
using AccountService.Core.Entities;
using AccountService.Core.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence.EfCore
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

            AddCqrs(services);
        }

        private static void AddCqrs(IServiceCollection services)
        {
            services.AddScoped<ICommands<Account>, EfCoreAccountCommands>();
            services.AddScoped<IQueries<Account>, EfAccountQueries>();
        }
    }
}