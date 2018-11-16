using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountService.Controllers
{
    [ApiController]
    [Produces("text/json")]
    [Route("api/[controller]")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly ICommands<Account> _commands;
        private readonly IQueries<Account> _queries;

        public AccountsController(ICommands<Account> commands,
            IQueries<Account> queries,
            ILogger<AccountsController> logger)
        {
            _logger = logger;
            _commands = commands;
            _queries = queries;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            try
            {
                return _queries.GetAll()
                    .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("Couldn't get all Accounts", ex);
                return StatusCode(503);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Account> Get(ulong id)
        {
            try
            {
                var result = _queries.GetBy(id);

                if (result != null)
                {
                    _logger.LogInformation($"Found Account with ID {id}");
                    return result;
                }

                _logger.LogWarning($"Couldn't find Account with ID {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Invalid request for Account with ID {id}", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<Account> Add(Account account)
        {
            try
            {
                _commands.Add(account);
                return CreatedAtAction("Add", new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't create Account with ID {account.Id}", ex);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Account> Update(ulong id, Account account)
        {
            try
            {
                _commands.Update(id, account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't update Account with ID {id}", ex);
                return BadRequest();
            }

            _logger.LogInformation($"Updated Account with ID {id}");
            return NoContent();
        }

        [HttpGet("spec")]
        public ActionResult<IEnumerable<Account>> SpecificationTest()
        {
            return Ok();
        }
    }
}
