using AccountService.Core.Entities;
using AccountService.Core.Search;
using System;
using System.Linq.Expressions;

namespace AccountService.Search
{
    public class AccountIdMatchesSpecification : ISpecification<Account>
    {
        public ulong Id { get; }

        public AccountIdMatchesSpecification(ulong id)
        {
            Id = id;
        }

        public bool IsSatisfiedBy(Account account) => ToExpression().Compile()
            .Invoke(account);

        public Expression<Func<Account, bool>> ToExpression() =>
            x => x.Id == Id;

        public ISpecification<Account> And(ISpecification<Account> specification)
        {
            throw new NotImplementedException();
        }

        public ISpecification<Account> Or(ISpecification<Account> specification)
        {
            throw new NotImplementedException();
        }

        public ISpecification<Account> Not(ISpecification<Account> specification)
        {
            throw new NotImplementedException();
        }
    }
}