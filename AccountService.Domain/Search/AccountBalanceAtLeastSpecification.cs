using AccountService.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace AccountService.Domain.Search
{
    public class AccountBalanceAtLeastSpecification : Specification<Account>
    {
        public decimal Balance { get; }

        public override Expression<Func<Account, bool>> ToExpression() =>
            x => x.Balance >= Balance;

        public AccountBalanceAtLeastSpecification(decimal balance) =>
            Balance = balance;
    }
}
