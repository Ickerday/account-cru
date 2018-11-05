using Intro.Application.Services;
using Intro.Persistence;
using Moq;
using Xunit;

namespace IntroTests
{
    public class AccountServiceTests
    {
        private readonly AccountingContext _context;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _context = Mock.Of<AccountingContext>();
            _service = new AccountService(_context);
        }

        [Fact(Skip ="Unwritten")]
        public void Service__UsesAccountingContext()
        {

        }

        [Fact(Skip = "Unwritten")]
        public void Service__AddsNewAccount()
        {

        }

        [Fact(Skip = "Unwritten")]
        public void Service__Should()
        {

        }
    }
}
