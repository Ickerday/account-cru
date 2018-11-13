namespace Intro.Persistence
{
    public interface IDbInfrastructure<out TCollection>
    {
        TCollection Accounts { get; }
    }
}
