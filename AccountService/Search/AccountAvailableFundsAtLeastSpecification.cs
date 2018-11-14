using AccountService.Core.Entities;
using AccountService.Core.Search;
using System;
using System.Linq.Expressions;

namespace AccountService.Search
{
    public class AccountAvailableFundsAtLeastSpecification : Specification<Account>
    {
        public decimal AvailableFunds { get; }

        public AccountAvailableFundsAtLeastSpecification(decimal availableFunds) =>
            AvailableFunds = availableFunds;

        public override Expression<Func<Account, bool>> ToExpression() =>
            x => x.AvailableFunds >= AvailableFunds;
    }
}