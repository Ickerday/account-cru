using Intro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intro.Persistence.EfCore
{
    public class AccountingContext : DbContext, IDbInfrastructure<DbSet<Account>>
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
