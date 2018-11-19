using System;
using System.Linq.Expressions;

namespace AccountService.Application.Search
{
    public interface ISpecificationBuilder<T>
    {
        Expression<Func<T, bool>> Build();
    }
}