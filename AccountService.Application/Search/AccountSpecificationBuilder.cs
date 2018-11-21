using AccountService.Application.Interfaces;
using AccountService.Domain.Exceptions.Specification;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!_predicates.Any())
                throw new InvalidSpecificationException("No predicates provided!");

            foreach (var predicate in _predicates)
                _predBuilder.Extend(predicate, PredicateOperator.And);

            return _predBuilder;
        }

        public AccountSpecificationBuilder(bool defaultExpression = true)
        {
            _predicates = new List<Expression<Func<Account, bool>>>();
            _predBuilder = PredicateBuilder.New<Account>(defaultExpression);
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

            _predicates.Add(x => x.Name.Contains(name, StringComparison.InvariantCulture));
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
