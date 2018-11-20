using MongoDB.Bson.Serialization.Attributes;

namespace AccountService.Domain.Entities
{
    public class Account : IEntity<ulong>
    {
        [BsonId]
        public ulong Id { get; set; } = 0UL;
        public string Name { get; set; } = string.Empty;
        public decimal AvailableFunds { get; set; } = decimal.Zero;
        public decimal Balance { get; set; } = decimal.Zero;
        public bool HasCard { get; set; }

        public static bool IsValid(Account account) => account.Id > ulong.MinValue
                                                       && !string.IsNullOrWhiteSpace(account.Name)
                                                       && account.AvailableFunds <= account.Balance;

        public static void MapAccount(Account newAccount, Account oldAccount)
        {
            oldAccount.Name = newAccount.Name;
            oldAccount.AvailableFunds = newAccount.AvailableFunds;
            oldAccount.Balance = newAccount.Balance;
            oldAccount.HasCard = newAccount.HasCard;
        }
    }
}
