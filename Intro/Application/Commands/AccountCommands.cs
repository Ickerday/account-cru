using Intro.Core.Entities;
using Intro.Persistence;

namespace Intro.Application.Commands
{
    public interface IAccountCommands
    {
        void Update(Account account);
        void Add(Account account);
    }

    public class AccountCommands : IAccountCommands
    {
        private readonly AccountingContext _context;

        public AccountCommands(AccountingContext context)
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