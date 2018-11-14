using System;
using System.Linq.Expressions;

namespace AccountService.Core.Search
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public NotSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
