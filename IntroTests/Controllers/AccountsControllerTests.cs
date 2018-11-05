using Intro.Application.Services;
using Intro.Controllers;
using Intro.Core.Entities;
using Moq;
using Xunit;

namespace IntroTests.Controllers
{
    public class AccountsControllerTests
    {
        private readonly IAccountService _accountService;
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _accountService = Mock.Of<IAccountService>();
            _controller = new AccountsController(_accountService);
        }

        [Theory]
        [InlineData(0L, "", 0, 0, false)]
        [InlineData(49203841098409218L, "test_value1", 123, 123, true)]
        public void Controller__ShouldAddAccounts(ulong number, string name, decimal availableFunds, decimal balance, bool hasCard)
        {
            var account = Mock.Of<Account>(x => x.Name == name
                && x.Number == number
                && x.AvailableFunds == availableFunds
                && x.Balance == balance
                && x.HasCard == hasCard);


            var result = _controller.Add(account);

            Assert.Equal(result.Value, account);
        }

    }
}
