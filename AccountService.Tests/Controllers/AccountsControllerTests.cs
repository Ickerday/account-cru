using AccountService.Application.Interfaces;
using AccountService.Controllers;
using AccountService.Domain.Entities;
using AccountService.Tests.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;

namespace AccountService.Tests.Controllers
{
    public class AccountsControllerTests
    {
        private AccountsController _controller;

        [Theory]
        [InlineData(ulong.MinValue, "yahoo", 0, 0, false)]
        [InlineData(ulong.MaxValue, "test_value1", 123, 123, true)]
        public void Controller__ShouldAddAccounts(ulong id, string name, decimal availableFunds, decimal balance, bool hasCard)
        {
            // Arrange
            var account = AccountHelpers.GetMockAccount(id, name, availableFunds, balance, hasCard);

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockCommands = new Mock<ICommands<Account>>();
            var mockQueries = new Mock<IQueries<Account>>();

            _controller = new AccountsController(mockCommands.Object, mockQueries.Object, mockLogger.Object);

            // Act
            var response = _controller.Add(account);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void Controller__UpdatesAccounts()
        {
            // Arrange
            const ulong id = ulong.MaxValue;
            const bool hasCard = true;

            var newAccount = AccountHelpers.GetMockAccount(id, "test_value2", 123123M, 456456M, hasCard);

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockCommands = new Mock<ICommands<Account>>();
            var mockQueries = new Mock<IQueries<Account>>();
            mockQueries.Setup(x => x.GetBy(id))
                .Returns(newAccount);

            _controller = new AccountsController(mockCommands.Object, mockQueries.Object, mockLogger.Object);

            // Act
            var updateResponse = _controller.Update(id, newAccount);

            // Assert
            Assert.IsType<NoContentResult>(updateResponse.Result);
            mockCommands.Verify(x => x.Update(id, newAccount));
        }

        [Fact]
        public void Controller__GetsAllAccounts()
        {
            // Arrange
            var accountList = new[] { AccountHelpers.GetMockAccount(0UL, "test1", 123M, 456M, false) };

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockCommands = new Mock<ICommands<Account>>();
            var mockQueries = new Mock<IQueries<Account>>();
            mockQueries.Setup(x => x.GetAll())
                .Returns(accountList);

            _controller = new AccountsController(mockCommands.Object, mockQueries.Object, mockLogger.Object);

            // Act
            var response = _controller.Get();

            // Assert
            Assert.Equal(accountList, response.Value);
        }

        [Theory]
        [InlineData(ulong.MinValue, "", 312098310983091, 10293821093810983, false)]
        public void Controller_ShouldGetBySpec(ulong? id, string name, decimal availableFunds, decimal balance, bool? hasCard)
        {
            // Arrange
            var accountList = new[] { AccountHelpers.GetMockAccount(id, name, availableFunds, balance, hasCard) };

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockCommands = new Mock<ICommands<Account>>();
            var mockQueries = new Mock<IQueries<Account>>();
            mockQueries.Setup(x => x.FindWith(It.IsAny<ISpecificationBuilder<Account>>()))
                .Returns(accountList);

            _controller = new AccountsController(mockCommands.Object, mockQueries.Object, mockLogger.Object);

            // Act
            var result = _controller.GetBySpecification(id, name, availableFunds, balance, hasCard);

            // Assert
            mockQueries.Verify(x => x.FindWith(It.IsAny<ISpecificationBuilder<Account>>()));
            Assert.Contains(accountList.Single(), result.Value);
        }
    }
}
