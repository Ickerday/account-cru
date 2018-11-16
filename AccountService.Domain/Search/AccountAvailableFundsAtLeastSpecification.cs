using System;
using System.Linq.Expressions;
using AccountService.Domain.Entities;

namespace AccountService.Domain.Search
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