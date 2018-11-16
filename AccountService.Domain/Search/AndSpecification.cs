using System;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Domain.Search
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            // This is the weirdest thing ever
            // https://stackoverflow.com/a/15592610
            var andExpression = Expression.AndAlso(leftExpression.Body,
                Expression.Invoke(rightExpression, leftExpression.Parameters.SingleOrDefault()));
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.SingleOrDefault());
        }
    }
}
