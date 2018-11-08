using System.Collections.Generic;

namespace Intro.Application.Queries
{
    public interface IQueries<out TEntity, in TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetBy(TKey id);
    }
}
