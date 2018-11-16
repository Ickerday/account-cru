using System;
using System.Linq.Expressions;
using AccountService.Domain.Entities;

namespace AccountService.Domain.Search
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