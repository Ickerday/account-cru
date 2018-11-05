using Intro.Core.Entities;
using Intro.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Intro.Application.Services
{
    public interface IAccountService
    {
        void Add(Account account);
        IEnumerable<Account> Find(ulong number);
        void Update(Account account);
    }

    public class AccountService : IAccountService
    {
        private readonly AccountingContext _context;

        public AccountService(AccountingContext context) => 
            _context = context;

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

        public IEnumerable<Account> Find(ulong number) => _context.Accounts
                .Where(x => x.Number.Equals(number));
    }
}
