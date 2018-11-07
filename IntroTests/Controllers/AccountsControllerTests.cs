using Intro.Application.Services;
using Intro.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static IntroTests.AccountHelpers;

namespace IntroTests.Controllers
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
            var account = GetMockAccount(id, name, availableFunds, balance, hasCard);

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockService = new Mock<IAccountService>();

            _controller = new AccountsController(mockService.Object, mockLogger.Object);

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

            const string oldName = "test_value1";
            const int oldFunds = 123;
            const int oldBalance = 456;

            const string newName = "test_value2";
            const int newFunds = 123123;
            const int newBalance = 456456;

            var oldAccount = GetMockAccount(id, oldName, oldFunds, oldBalance, hasCard);
            var newAccount = GetMockAccount(id, newName, newFunds, newBalance, hasCard);

            var mockLogger = new Mock<ILogger<AccountsController>>();
            var mockService = new Mock<IAccountService>();
            mockService.Setup(x => x.GetBy(id))
                .Returns(newAccount);

            _controller = new AccountsController(mockService.Object, mockLogger.Object);

            // Act
            _controller.Add(oldAccount);
            var updateResponse = _controller.Update(id, newAccount);
            var updatedAccount = _controller.Get(id);

            // Assert
            Assert.IsType<NoContentResult>(updateResponse.Result);
            Assert.Equal(newAccount, updatedAccount.Value);
        }

        [Fact]
        public void Controller__GetsAllAccounts()
        {
            {
                // Arrange
                var accountList = new[] { GetMockAccount(0, "test1", 123, 456, false) };

                var mockLogger = new Mock<ILogger<AccountsController>>();
                var mockService = new Mock<IAccountService>();
                mockService.Setup(x => x.GetAccounts())
                    .Returns(accountList);

                _controller = new AccountsController(mockService.Object, mockLogger.Object);

                // Act
                var response = _controller.Get();

                // Assert
                Assert.Equal(accountList, response.Value);
            }
        }

    }

}
