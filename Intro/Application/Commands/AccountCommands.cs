using Intro.Core.Entities;
using Intro.Persistence;

namespace Intro.Application.Commands
{
    public interface IAccountCommands
    {

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
    }
}