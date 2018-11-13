using MongoDB.Bson.Serialization.Attributes;

namespace AccountService.Core.Entities
{
    public class Account : IEntity<ulong>
    {
        [BsonId]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public decimal AvailableFunds { get; set; } = 0m;
        public decimal Balance { get; set; } = 0m;
        public bool HasCard { get; set; } = false;
    }

    public static class AccountHelpers
    {
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
