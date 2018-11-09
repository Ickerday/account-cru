using Intro.Core.Entities;

namespace Intro.Application.Commands
{
    public interface IAccountCommands
    {
        void Update(Account account);
        void Add(Account account);
    }
}
