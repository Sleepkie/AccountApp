using AccountApp.Domain.Interfaces;
using AccountApp.Domain.Models;
using AccountApp.Domain.Models.DTO;
using AccountApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AccountAppAPI.Controllers
{
    [Route("account/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        private IAccountValidator _validator;

        public AccountController(IAccountService accountService, IAccountValidator validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();

            if (accounts.Count() == 0)
            {
                return NoContent();
            }

            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetAccountAsync(int id)
        {
            var account = await _accountService.GetAccountAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetFilteredAsync([FromQuery] AccountFilter filter)
        {
            var accounts = await _accountService.GetFilteredAsync(filter);

            if (accounts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccountAsync([FromBody] AccountDTO accountDTO)
        {
            if (!_validator.Validate(accountDTO))
            {
                return BadRequest();
            }
            
            Account account = new Account()
            {
                AccountNumber = accountDTO.AccountNumber,
                OpeningDate = DateTime.ParseExact(accountDTO.OpeningDate,"yyyy-MM-dd", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(accountDTO.ExpirationDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Address = new() { City = accountDTO.Address.City, Building = accountDTO.Address.Building, Street = accountDTO.Address.Street},
                Area = accountDTO.Area
            };

            for (int i = 0; i < accountDTO.Residents.Count(); i++)
            {
                account.Residents.Add(new Resident { FullName = accountDTO.Residents[i].FullName });
            }

            var added = await _accountService.AddAccountAsync(account);

            if (!added)
            {
                return BadRequest();
            }

            return Ok(account);
        }

        [HttpPut]
        public async Task<IActionResult> EditAccountAsync([FromBody] AccountDTO accountDTO)
        {
            
            if (!_validator.Validate(accountDTO))
            {
                return BadRequest();
            }
            
            string openingDateOnly = accountDTO.OpeningDate.Substring(0, 10);
            string expirationDateOnly = accountDTO.ExpirationDate.Substring(0, 10);

            Account account = new Account()
            {
                AccountNumber = accountDTO.AccountNumber,
                OpeningDate = DateTime.ParseExact(openingDateOnly, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact(expirationDateOnly, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Address = new() { City = accountDTO.Address.City, Building = accountDTO.Address.Building, Street = accountDTO.Address.Street },
                Area = accountDTO.Area
            };

            for (int i = 0; i < accountDTO.Residents.Count(); i++)
            {
                account.Residents.Add(new Resident { FullName = accountDTO.Residents[i].FullName });
            }

            var edited = await _accountService.EditAccountAsync(account);

            if (!edited)
            {
                return BadRequest();
            }

            return Ok(account);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _accountService.DeleteAccountAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
