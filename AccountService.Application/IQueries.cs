using System.Collections.Generic;
using AccountService.Application.Search;

namespace AccountService.Application
{
    public interface IQueries<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetBy(ulong id);
        IEnumerable<TEntity> FindWith(Specification<TEntity> specification);
    }
}