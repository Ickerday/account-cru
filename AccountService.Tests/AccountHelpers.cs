using AccountService.Domain.Entities;
using Moq;

namespace AccountService.Tests
{
    internal static class AccountHelpers
    {
        internal static Account GetMockAccount(ulong? id, string name, decimal? availableFunds, decimal? balance, bool? hasCard) =>
            Mock.Of<Account>(x => x.Name == name
                && x.Id == id.Value
                && x.AvailableFunds == availableFunds.Value
                && x.Balance == balance.Value
                && x.HasCard == hasCard.Value);
    }
}
