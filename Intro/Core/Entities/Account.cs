namespace Intro.Core.Entities
{
    public class Account
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public decimal AvailableFunds { get; set; } = 0m;
        public decimal Balance { get; set; } = 0m;
        public bool HasCard { get; set; } = false;
    }
}
