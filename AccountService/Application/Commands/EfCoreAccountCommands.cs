using AccountService.Core.Entities;
using AccountService.Persistence.EfCore;

namespace AccountService.Application.Commands
{
    public class EfCoreAccountCommands : IAccountCommands
    {
        private readonly AccountingContext _context;

        public EfCoreAccountCommands(AccountingContext context)
        {
            _context = context;
        }

        public void Add(Account account)
        {
            _context.Accounts
                .Add(account);

            _context.SaveChanges();
        }

        public void Update(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
        }
    }
}