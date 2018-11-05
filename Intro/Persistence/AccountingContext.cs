using Intro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intro.Persistence
{
    public class AccountingContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options)
        {
        }

    }
}
