using AccountService.Core.Entities;

namespace AccountService.Application.Commands
{
    public interface IAccountCommands
    {
        void Update(Account account);
        void Add(Account account);
    }
}
