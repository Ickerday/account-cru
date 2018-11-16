using System;
using System.Linq.Expressions;
using AccountService.Domain.Entities;

namespace AccountService.Domain.Search
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
