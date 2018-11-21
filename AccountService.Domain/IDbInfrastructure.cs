namespace AccountService.Domain
{
    public interface IDbInfrastructure<out TCollection>
    {
        TCollection Accounts { get; }
    }
}
