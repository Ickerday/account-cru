using AccountService.Application.Accounts.Commands;
using AccountService.Application.Accounts.Queries;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AccountService.Persistence
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