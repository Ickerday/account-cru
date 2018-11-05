namespace Intro.Core.Entities
{
    public class Account
    {
        public ulong Number { get; set; }
        public string Name { get; set; }
        public decimal AvailableFunds { get; set; }
        public decimal Balance { get; set; }
        public bool HasCard { get; set; }
    }
}
