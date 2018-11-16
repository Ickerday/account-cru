using AccountService.Domain.Search;
using System.Collections.Generic;

namespace AccountService.Application.Interfaces
{
    public interface IQueries<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetBy(ulong id);
        IEnumerable<TEntity> FindWith(Specification<TEntity> specification);
    }
}