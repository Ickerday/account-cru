using System;
using System.Linq.Expressions;

namespace AccountService.Application.Interfaces
{
    public interface ISpecificationBuilder<T>
    {
        Expression<Func<T, bool>> Build();
    }
}