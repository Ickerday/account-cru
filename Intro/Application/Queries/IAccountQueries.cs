using System.Collections.Generic;
using Intro.Core.Entities;

namespace Intro.Application.Queries
{
    public interface IAccountQueries
    {
        IEnumerable<Account> GetAll();
        Account GetBy(ulong id);
    }
}