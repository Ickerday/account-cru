using Intro.Core.Entities;
using Intro.Persistence;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Intro.Application.Queries
{
    public interface IAccountQueries
    {
        IEnumerable<Account> GetAll();
        Account GetBy(ulong id);
    }

    public class AccountQueries : IAccountQueries
    {
        private readonly ILogger<IAccountQueries> _logger;
        private readonly AccountingContext _context;

        public AccountQueries(AccountingContext context, ILogger<IAccountQueries> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Account> GetAll() => _context.Accounts
            .ToArray();

        public Account GetBy(ulong id) => _context.Accounts
            .Find(id);
    }
}