using AccountService.Domain;
using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Persistence.EfCore
{
    public sealed class AccountingContext : DbContext, IDbInfrastructure<DbSet<Account>>
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
