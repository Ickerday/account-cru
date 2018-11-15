using AccountService.Core.Entities;
using AccountService.Core.Search;
using System;
using System.Linq.Expressions;

namespace AccountService.Search
{
    public class AccountBalanceLessThanSpecification : Specification<Account>
    {
        public AccountBalanceLessThanSpecification(decimal balance) =>
            Balance = balance;

        public decimal Balance { get; }

        public override Expression<Func<Account, bool>> ToExpression() =>
            x => x.Balance < Balance;
    }
}
