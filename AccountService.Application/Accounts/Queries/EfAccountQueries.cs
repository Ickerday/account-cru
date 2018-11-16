using AccountService.Domain.Entities;
using AccountService.Persistence.EfCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AccountService.Application.Interfaces;
using AccountService.Domain.Exceptions.Account;
using AccountService.Domain.Search;

namespace AccountService.Application.Accounts.Queries
{
    public class EfAccountQueries : IQueries<Account>
    {
        private readonly AccountingContext _context;
        private readonly ILogger<EfAccountQueries> _logger;

        public EfAccountQueries(AccountingContext context, ILogger<EfAccountQueries> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Account> GetAll()
        {
            _logger.LogInformation("Getting all Accounts");
            return _context.Accounts
                .ToArray();
        }

        public Account GetBy(ulong id)
        {
            _logger.LogInformation($"Searching for Account with ID {id}");
            var result = _context.Accounts
                .Find(id);

            if (result == null)
                throw new AccountNotFoundException($"No Account with ID {id} found");

            return result;
        }

        public IEnumerable<Account> FindWith(Specification<Account> specification)
        {
            _logger.LogInformation($"Searching for Accounts following a {specification.GetType().FullName}");
            return _context.Accounts
                .Where(specification.ToExpression())
                .ToArray();
        }
    }
}