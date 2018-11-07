using Intro.Application.Exceptions;
using Intro.Application.Services;
using Intro.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Intro.Controllers
{
    [Produces("text/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            try
            {
                return _accountService.GetAccounts()
                    .ToArray();
            }
            catch
            {
                _logger.LogWarning("Couldn't get all Accounts");
                return StatusCode(503);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Account> Get(ulong id)
        {
            try
            {
                var result = _accountService.GetBy(id);

                if (result != null)
                {
                    _logger.LogInformation($"Found Account with ID {id}");
                    return result;
                }

                _logger.LogWarning($"Couldn't find Account with ID {id}");
                return NotFound();
            }
            catch
            {
                _logger.LogWarning($"Invalid request for Account with ID {id}");
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<Account> Add(Account account)
        {
            try
            {
                _accountService.Add(account);
                _logger.LogInformation($"Created Account with ID {account.Id}");
                return CreatedAtAction("Add", new { id = account.Id }, account);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Account> Update(ulong id, Account account)
        {
            try
            {
                _accountService.Update(id, account);
                _logger.LogInformation($"Updated Account with ID {id}");
            }
            catch (AccountNotFoundException)
            {
                _logger.LogWarning($"Couldn't update Account with ID {id}");
                return BadRequest();
            }

            // log here
            return NoContent();
        }
    }
}