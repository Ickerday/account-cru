using AccountService.Core.Entities;
using Moq;
using Xunit;

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

        internal static void AreSame(Account expected, Account actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.AvailableFunds, actual.AvailableFunds);
            Assert.Equal(expected.Balance, actual.Balance);
            Assert.Equal(expected.HasCard, actual.HasCard);
        }
    }
}
