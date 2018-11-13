using Intro.Core.Entities;
using Intro.Persistence.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace Intro.Application.Queries
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