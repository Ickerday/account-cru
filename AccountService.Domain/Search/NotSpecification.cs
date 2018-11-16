using System;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Domain.Search
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _right;

        public NotSpecification(Specification<T> right)
        {
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var rightExpression = _right.ToExpression();

            var orExpression = Expression.Not(rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(orExpression, rightExpression.Parameters.SingleOrDefault());
        }
    }
}
