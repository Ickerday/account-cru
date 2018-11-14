using AccountService.Core.Entities;
using AccountService.Core.Search;
using System;
using System.Linq.Expressions;

namespace AccountService.Search
{
    public class AccountAvailableFundsLessThanSpecification : Specification<Account>
    {
        public decimal AvailableFunds { get; }

        public AccountAvailableFundsLessThanSpecification(decimal availableFunds) =>
            AvailableFunds = availableFunds;

        public override Expression<Func<Account, bool>> ToExpression() =>
            x => x.AvailableFunds < AvailableFunds;
    }
}