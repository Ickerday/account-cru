using Intro.Core.Entities;
using Moq;

namespace AccountService.Tests
{
    internal static class AccountHelpers
    {
        internal static Account GetMockAccount(ulong id, string name, decimal availableFunds, decimal balance, bool hasCard) =>
            Mock.Of<Account>(x => x.Name == name
                && x.Id == id
                && x.AvailableFunds == availableFunds
                && x.Balance == balance
                && x.HasCard == hasCard);
    }
}
