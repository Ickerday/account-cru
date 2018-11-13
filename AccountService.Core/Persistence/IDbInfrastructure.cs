namespace AccountService.Core.Persistence
{
    public interface IDbInfrastructure<out TCollection>
    {
        TCollection Accounts { get; }
    }
}
