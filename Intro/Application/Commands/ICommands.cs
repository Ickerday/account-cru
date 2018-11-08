namespace Intro.Application.Commands
{
    public interface ICommands<in TEntity>
    {
        void Update(TEntity account);
        void Add(TEntity account);
    }
}
