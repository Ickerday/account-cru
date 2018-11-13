using System.Collections.Generic;
using System.Linq;
using AccountService.Core.Entities;
using AccountService.Persistence.EfCore;

namespace AccountService.Application.Queries
{
    public class EfAccountQueries : IAccountQueries
    {
        private readonly AccountingContext _context;

        public EfAccountQueries(AccountingContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll() => _context.Accounts
            .ToArray();

        public Account GetBy(ulong id) => _context.Accounts
            .Find(id);
    }
}