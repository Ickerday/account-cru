using AccountService.Application.Search;
using AccountService.Tests.Common;
using System.Linq;
using Xunit;

namespace AccountService.Application.Tests.Search
{
    public class AccountSpecificationBuilderTests
    {
        private AccountSpecificationBuilder _builder;

        [Theory]
        [InlineData(100UL)]
        [InlineData(10948309850293823409UL)]
        [InlineData(ulong.MaxValue)]
        [InlineData(ulong.MinValue)]
        public void Builder__ShouldBuildCorrectIdPredicate(ulong id)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(id),
                AccountHelpers.GetMockAccount(id - 10),
                AccountHelpers.GetMockAccount(id - 20)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithId(id);
            var predicate = _builder.Build()
                .Compile();

            var result = accounts.Where(predicate);

            // Assert
            Assert.All(result, x => Assert.Equal(id, x.Id));
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("lhf243iufhc3o8e7c2r8c7hq38ocqhe87d97rhduh9wu f8r fw9fhv9s8d fhv98f vhw98r h398h")]
        [InlineData("abacababacababacababacababacababacababacababacababacababacababacababacababacab")]
        [InlineData("123123123123123123123123123123123123123123123123123123123123123123123123123123")]
        public void Builder__ShouldBuildCorrectNamePredicate(string name)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(name: name),
                AccountHelpers.GetMockAccount(name: "not")
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithName(name);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.Contains(name, x.Name));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectBalanceEqualToPredicate(decimal balance)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(balance: balance),
                AccountHelpers.GetMockAccount(balance: balance + 1),
                AccountHelpers.GetMockAccount(balance: balance - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithBalanceEqualTo(balance);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.Equal(balance, x.Balance));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectBalanceMoreOrEqualToPredicate(decimal balance)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(balance: balance),
                AccountHelpers.GetMockAccount(balance: balance + 1),
                AccountHelpers.GetMockAccount(balance: balance - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithBalanceMoreOrEqualTo(balance);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.True(x.Balance >= balance));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectBalanceLessOrEqualToPredicate(decimal balance)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(balance: balance),
                AccountHelpers.GetMockAccount(balance: balance + 1),
                AccountHelpers.GetMockAccount(balance: balance - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithBalanceLessOrEqualTo(balance);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.True(x.Balance <= balance));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectAvailableFundsEqualToPredicate(decimal availableFunds)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(availableFunds: availableFunds),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds + 1),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithAvailableFundsEqualTo(availableFunds);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.Equal(availableFunds, x.AvailableFunds));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectAvailableFundsLessOrEqualToPredicate(decimal availableFunds)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(availableFunds: availableFunds),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds + 1),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithAvailableFundsLessOrEqualTo(availableFunds);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.True(x.AvailableFunds <= availableFunds));
        }

        [Theory]
        [InlineData(-2000000)]
        [InlineData(-2000)]
        [InlineData(0)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Builder__ShouldBuildCorrectAvailableFundsMoreOrEqualToPredicate(decimal availableFunds)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(availableFunds: availableFunds),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds + 1),
                AccountHelpers.GetMockAccount(availableFunds: availableFunds - 1)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithAvailableFundsMoreOrEqualTo(availableFunds);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.True(x.AvailableFunds >= availableFunds));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Builder__ShouldBuildCorrectHasCardPredicate(bool hasCard)
        {
            // Arrange
            var accounts = new[]
            {
                AccountHelpers.GetMockAccount(hasCard: hasCard),
                AccountHelpers.GetMockAccount(hasCard: !hasCard)
            };

            _builder = new AccountSpecificationBuilder();

            // Act
            _builder.WithCard(hasCard);
            var predicate = _builder.Build()
                .Compile();

            var results = accounts.Where(predicate);

            // Assert
            Assert.All(results, x => Assert.Equal(hasCard, x.HasCard));
        }
    }
}