using System;
using System.Linq.Expressions;

namespace AccountService.Core.Search
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters);
        }
    }
}
