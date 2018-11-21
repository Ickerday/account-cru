using AccountService.Application.Interfaces;
using AccountService.Application.Search;
using AccountService.Domain.Entities;
using AccountService.Domain.Exceptions.Specification;
using Microsoft.AspNetCore.Http;
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
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Account> Get(ulong id)
        {
            try
            {
                var idSpec = new AccountSpecificationBuilder()
                    .WithId(id);

                var result = _queries.FindWith(idSpec)
                    .FirstOrDefault();

                if (result == null)
                {
                    _logger.LogWarning($"Couldn't find Account with ID {id}");
                    return NotFound();
                }

                _logger.LogInformation($"Found Account with ID {id}");
                return result;
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
        public ActionResult<IEnumerable<Account>> GetBySpecification([FromQuery] ulong? id,
            [FromQuery] string name, [FromQuery] decimal? availableFunds,
            [FromQuery] decimal? balance, [FromQuery] bool? hasCard)
        {
            try
            {
                var spec = PrepareSpecification(id, name, availableFunds, balance, hasCard);

                return _queries.FindWith(spec)
                    .ToArray();
            }
            catch (InvalidSpecificationException specEx)
            {
                _logger.LogError("Wrong specification provided", specEx);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        private static ISpecificationBuilder<Account> PrepareSpecification(ulong? id,
            string name, decimal? availableFunds, decimal? balance, bool? hasCard)
        {
            var spec = new AccountSpecificationBuilder(false);

            if (id.HasValue)
                spec = spec.WithId(id);
            if (!string.IsNullOrWhiteSpace(name))
                spec = spec.WithName(name);
            if (availableFunds.HasValue)
                spec = spec.WithAvailableFundsEqualTo(availableFunds);
            if (balance.HasValue)
                spec = spec.WithBalanceEqualTo(balance);
            if (hasCard.HasValue)
                spec = spec.WithCard(hasCard);
            return spec;
        }
    }
}
