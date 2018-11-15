using System;
using System.Linq.Expressions;

namespace AccountService.Core.Search
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return ToExpression().Compile()
                .Invoke(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification) =>
            new AndSpecification<T>(this, specification);

        public Specification<T> Or(Specification<T> specification) =>
            throw new NotImplementedException();

        public Specification<T> Not(Specification<T> specification) =>
            throw new NotImplementedException();
    }
}
