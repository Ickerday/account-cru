using AccountService.Domain.Exceptions.Specification;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Account = AccountService.Domain.Entities.Account;

namespace AccountService.Application.Search
{
    public class AccountSpecificationBuilder : ISpecificationBuilder<Account>
    {
        private readonly Expression<Func<Account, bool>> _predBuilder;

        private readonly IList<Expression<Func<Account, bool>>> _predicates;

        public Expression<Func<Account, bool>> Build()
        {
            foreach (var predicate in _predicates)
                _predBuilder.And(predicate);

            return _predBuilder;
        }

        public AccountSpecificationBuilder()
        {
            _predicates = new List<Expression<Func<Account, bool>>>();
            _predBuilder = PredicateBuilder.New<Account>(true);
        }

        public AccountSpecificationBuilder WithId(ulong? id)
        {
            if (!id.HasValue)
                throw new InvalidSpecificationException("Wrong ID specified");

            _predicates.Add(x => x.Id == id.Value);
            return this;
        }

        public AccountSpecificationBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidSpecificationException("Wrong Name specified");

            _predicates.Add(x => x.Name == name);
            return this;
        }

        public AccountSpecificationBuilder WithAvailableFunds(decimal? availableFunds)
        {
            if (!availableFunds.HasValue)
                throw new InvalidSpecificationException("Wrong AvailableFunds specified");

            _predicates.Add(x => x.AvailableFunds == availableFunds.Value);
            return this;
        }

        public AccountSpecificationBuilder WithBalance(decimal? balance)
        {
            if (!balance.HasValue)
                throw new InvalidSpecificationException("Wrong Balance specified");

            _predicates.Add(x => x.Balance == balance);
            return this;
        }

        public AccountSpecificationBuilder WithCard(bool? hasCard)
        {
            if (!hasCard.HasValue)
                throw new InvalidSpecificationException("Wrong HasCard specified");

            _predicates.Add(x => x.HasCard == hasCard.Value);
            return this;
        }
    }
}
