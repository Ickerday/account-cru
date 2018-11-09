using Intro.Application.Commands;
using Intro.Application.Queries;
using Intro.Application.Services;
using Intro.Core.Entities;
using Moq;
using Xunit;
using static IntroTests.AccountHelpers;

namespace IntroTests.Application.Services
{
    public class AccountServiceTests
    {
        private AccountService _service;

        [Fact(Skip = "Changing implementation")]
        public void Service__AddsNewAccount()
        {
            // Arrange
            const ulong id = 49203849218UL;
            var account = GetMockAccount(id, "test_value1", 123, 123, true);

            var mockQueries = new Mock<IAccountQueries>();
            var mockCommands = new Mock<IAccountCommands>();
            _service = new AccountService(mockQueries.Object, mockCommands.Object);

            // Act
            _service.Add(account);

            // Assert
            mockCommands.Verify(x => x.Add(account), Times.Once);
        }

        [Fact(Skip = "Changing implementation")]
        public void Service__GetsAllAccounts()
        {
            // Arrange
            var accountList = new[] {
                GetMockAccount(0987890UL, "test1", 123m, 456m, false),
                GetMockAccount(1987098UL, "test2", 789m, 1011m, true)
            };

            var mockQueries = new Mock<IAccountQueries>();
            var mockCommands = new Mock<IAccountCommands>();
            mockQueries.Setup(x => x.GetAll())
                .Returns(accountList);

            _service = new AccountService(mockQueries.Object, mockCommands.Object);

            // Act
            foreach (var account in accountList)
                _service.Add(account);

            // Assert
            mockCommands.Verify(x => x.Add(It.IsAny<Account>()), Times.Exactly(accountList.Length));
            Assert.Equal(_service.GetAll(), accountList);
        }

        [Fact(Skip = "Changing implementation")]
        public void Service__GetsAccountById()
        {
            // Arrange
            const ulong id = 49203841098409218UL;
            var account = GetMockAccount(id, "test_value1", 123, 123, true);

            var mockQueries = new Mock<IAccountQueries>();
            mockQueries.Setup(x => x.GetBy(id))
                .Returns(account);

            var mockCommands = new Mock<IAccountCommands>();
            _service = new AccountService(mockQueries.Object, mockCommands.Object);

            // Act
            var result = _service.GetBy(id);

            // Assert
            mockQueries.Verify(x => x.GetBy(id), Times.Once);
            Assert.Equal(account, result);
        }

        [Fact(Skip = "Changing implementation")]
        public void Service__UpdatesAccount()
        {
            // Arrange
            const ulong id = 49203841098409218UL;
            var newAccount = GetMockAccount(id, "test_value2", 123123, 456456, true);

            var mockQueries = new Mock<IAccountQueries>();
            mockQueries.Setup(x => x.GetBy(id))
                .Returns(newAccount);

            var mockCommands = new Mock<IAccountCommands>();
            _service = new AccountService(mockQueries.Object, mockCommands.Object);

            // Act
            _service.Update(id, newAccount);

            // Assert
            mockCommands.Verify(x => x.Update(newAccount), Times.Once);
        }
    }
}
