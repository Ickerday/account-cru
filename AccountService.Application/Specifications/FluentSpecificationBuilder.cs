using AccountService.Domain.Entities;
using AccountService.Domain.Exceptions.Specification;
using System.Linq;

namespace AccountService.Application.Specifications
{
    public class FluentSpecificationBuilder
    {
        private IQueryable<Account> _accounts;

        public FluentSpecificationBuilder(IQueryable<Account> accounts) => _accounts = accounts;

        public FluentSpecificationBuilder WithId(ulong id)
        {
            if (id <= 0)
                throw new InvalidSpecificationException("Wrong ID provided to specification");

            //
            return this;
        }

        public FluentSpecificationBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidSpecificationException("Wrong name provided to specification");

            //
            return this;
        }


    }
}
