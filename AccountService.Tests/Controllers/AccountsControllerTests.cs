using AccountService.Controllers;
using AccountService.Core.Commands;
using AccountService.Core.Entities;
using AccountService.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AccountService.Tests.Controllers
{
    public class AccountsControllerTests
    {
        private AccountsController _controller;

        [Theory]
        [InlineData(0UL, "yahoo", 0, 0, false)]
        [InlineData(49203841098409218UL, "test_value1", 123, 123, true)]
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
            const ulong id = 49203841098409218UL;
            const bool hasCard = true;

            var newAccount = AccountHelpers.GetMockAccount(id, "test_value2", 123123m, 456456m, hasCard);

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockCommands = new Mock<ICommands<Account>>();
            var mockQueries = new Mock<IQueries<Account>>();
            mockQueries.Setup(x => x.GetBy(id))
                .Returns(newAccount);

            _controller = new AccountsController(mockCommands.Object, mockQueries.Object, mockLogger.Object);

            // Act
            var updateResponse = _controller.Update(id, newAccount);
            var updatedAccount = _controller.Get(id).Value;

            // Assert
            Assert.IsType<NoContentResult>(updateResponse.Result);
            AccountHelpers.AreSame(newAccount, updatedAccount);
        }

        [Fact]
        public void Controller__GetsAllAccounts()
        {
            {
                // Arrange
                var accountList = new[] { AccountHelpers.GetMockAccount(0, "test1", 123, 456, false) };

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
        }

    }

}
