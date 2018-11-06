using Intro.Application.Services;
using Intro.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get() => _accountService.GetAccounts()
                .ToArray();

        [HttpGet("{id}")]
        public ActionResult<Account> Get(ulong id)
        {
            var result = _accountService.Find(id);

            if (result != null)
                return result;

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Account> Add(Account account)
        {
            _accountService.Add(account);
            return CreatedAtAction("Add", new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public ActionResult<Account> Update(ulong id, Account account)
        {
            _accountService.Update(id, account);
            return NoContent();
        }

    }
}