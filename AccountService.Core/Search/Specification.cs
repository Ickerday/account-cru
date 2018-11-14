using System;
using System.Linq.Expressions;
using AccountService.Core.Exceptions.Specification;

namespace AccountService.Core.Search
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            if (entity == null)
                throw new InvalidSpecificationException();
            return ToExpression().Compile()
                .Invoke(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }

        public ISpecification<T> Not(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }
    }



}
