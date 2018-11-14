using AccountService.Core.Entities;
using AccountService.Core.Search;
using System.Collections.Generic;

namespace AccountService.Core.Queries
{
    public interface IQueries<out TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetBy(ulong id);
        IEnumerable<TEntity> FindWith(ISpecification<Account> specification);
    }
}