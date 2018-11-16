namespace AccountService.Application.Interfaces
{
    public interface ICommands<in TEntity>
    {
        void Update(ulong id, TEntity entity);
        void Add(TEntity entity);
    }
}
