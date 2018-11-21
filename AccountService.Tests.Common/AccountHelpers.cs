using AccountService.Domain.Entities;
using Moq;

namespace AccountService.Tests.Common
{
    public static class AccountHelpers
    {
        public static Account GetMockAccount(ulong? id = 0, string name = "", decimal? availableFunds = 0, decimal? balance = 0, bool? hasCard = false) =>
            Mock.Of<Account>(x => x.Name == name
                && x.Id == id.Value
                && x.AvailableFunds == availableFunds.Value
                && x.Balance == balance.Value
                && x.HasCard == hasCard.Value);
    }
}
