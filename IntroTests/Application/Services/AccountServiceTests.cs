using Intro.Application.Services;
using Intro.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static IntroTests.AccountHelpers;

namespace IntroTests
{
    public class AccountServiceTests
    {
        private AccountService _service;

        [Fact]
        public void Service__AddsNewAccount()
        {
            // Arrange
            var id = 49203841098409218UL;
            var account = GetMockAccount(id, "test_value1", 123, 123, true);

            var options = new DbContextOptionsBuilder<AccountingContext>()
                .UseInMemoryDatabase(databaseName: "AddsNewAccount")
                .Options;

            // Act
            using (var context = new AccountingContext(options))
            {
                _service = new AccountService(context);
                _service.Add(account);

                // Assert
                Assert.Equal(context.Accounts.Find(id), account);
            }
        }

        [Fact]
        public void Service__GetsAllAccounts()
        {
            // Arrange
            var accountList = new[] {
                GetMockAccount(0987890UL, "test1", 123m, 456m, false),
                GetMockAccount(1987098UL, "test2", 789m, 010m, true)
            };

            var options = new DbContextOptionsBuilder<AccountingContext>()
                .UseInMemoryDatabase(databaseName: "GetsAllAccounts")
                .Options;

            // Act
            using (var context = new AccountingContext(options))
            {
                _service = new AccountService(context);

                foreach (var account in accountList)
                    _service.Add(account);

                // Assert
                Assert.Equal(_service.GetAccounts(), accountList);
            }
        }

        [Fact]
        public void Service__GetsAccountById()
        {
            // Arrange
            var id = 49203841098409218UL;
            var account = GetMockAccount(id, "test_value1", 123, 123, true);

            var options = new DbContextOptionsBuilder<AccountingContext>()
                .UseInMemoryDatabase(databaseName: "GetsAccountById")
                .Options;

            // Act
            using (var context = new AccountingContext(options))
            {
                _service = new AccountService(context);
                context.Add(account);

                // Assert
                Assert.Equal(_service.Find(id), account);
            }
        }

        [Fact]
        public void Service__UpdatesAccount()
        {
            // Arrange
            var id = 49203841098409218UL;
            var hasCard = true;

            var oldName = "test_value1";
            var oldFunds = 123;
            var oldBalance = 456;

            var newName = "test_value2";
            var newFunds = 123123;
            var newBalance = 456456;

            var oldAccount = GetMockAccount(id, oldName, oldFunds, oldBalance, hasCard);
            var newAccount = GetMockAccount(id, newName, newFunds, newBalance, hasCard);

            var options = new DbContextOptionsBuilder<AccountingContext>()
                .UseInMemoryDatabase(databaseName: "UpdatesAccount")
                .Options;

            // Act
            using (var context = new AccountingContext(options))
            {
                _service = new AccountService(context);

                context.Add(oldAccount);
                context.SaveChanges();

                _service.Update(id, newAccount);

                var updatedAccount = context.Accounts.Find(id);

                // Assert
                Assert.Equal(newAccount.Name, updatedAccount.Name);
                Assert.Equal(newAccount.Balance, updatedAccount.Balance);
                Assert.Equal(newAccount.AvailableFunds, updatedAccount.AvailableFunds);
                Assert.Equal(newAccount.HasCard, updatedAccount.HasCard);

            }
        }
    }
}
