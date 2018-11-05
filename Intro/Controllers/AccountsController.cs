using Intro.Application.Services;
using Intro.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Intro.Controllers
{
    [Produces("text/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{number}")]
        public ActionResult<Account> Get(ulong number)
        {
            var result = _accountService.Find(number)
                .FirstOrDefault();

            if (result != null)
                return result;

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Account> Add(Account account)
        {
            _accountService.Add(account);
            return account;
        }

        [HttpPut]
        public ActionResult<Account> Update(Account account)
        {

            return account;
        }

    }
}