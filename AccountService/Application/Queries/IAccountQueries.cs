using System.Collections.Generic;
using AccountService.Core.Entities;

namespace AccountService.Application.Queries
{
    public interface IAccountQueries
    {
        IEnumerable<Account> GetAll();
        Account GetBy(ulong id);
    }
}