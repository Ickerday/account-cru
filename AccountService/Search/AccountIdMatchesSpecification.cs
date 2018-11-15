using AccountService.Core.Entities;
using AccountService.Core.Search;
using System;
using System.Linq.Expressions;

namespace AccountService.Search
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