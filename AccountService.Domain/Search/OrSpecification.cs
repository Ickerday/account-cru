using System;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Domain.Search
{
    public class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            return leftExpression && rightExpression;

            var orExpression = Expression.OrElse(leftExpression.Body,
                Expression.Invoke(rightExpression, leftExpression.Parameters.SingleOrDefault()));
            return Expression.Lambda<Func<T, bool>>
                (orExpression, leftExpression.Parameters.SingleOrDefault());
        }
    }
}
