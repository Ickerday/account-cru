using AccountService.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace AccountService.Domain.Search
{
    public class AccountIdMatchesSpecification : Specification<Account>
    {
        public ulong Id { get; }

        public AccountIdMatchesSpecification(ulong id) =>
            Id = id;

        public override Expression<Func<Account, bool>> ToExpression() =>
            x => x.Id == Id;
    }
}