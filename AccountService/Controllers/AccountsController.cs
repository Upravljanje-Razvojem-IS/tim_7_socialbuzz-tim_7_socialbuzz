using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccountService.Entities;
using AccountService.DTOs.Account;
using FluentValidation.Results;
using AccountService.Validators.Role;
using AutoMapper;
using AccountService.Exceptions;
using AccountService.Services.Accounts;
using Microsoft.AspNetCore.Authorization;

namespace AccountService.Controllers
{
    [Route("api/accounts")]
    [Authorize]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            this.mapper = mapper;
        }

        // GET: api/Accounts
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts()
        {
            return Ok(mapper.Map<IEnumerable<AccountBasicResponseDTO>>(_accountService.GetAllAsync()));
        }

        /// <summary>
        /// Returns an account.
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>A role</returns>
        /// <response code="200">Account data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Account was not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountFullResponseDTO>> GetAccount(Guid id)
        {
            Account account;

            try
            {
                account = await _accountService.GetByIdAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(mapper.Map<AccountFullResponseDTO>(account));
        }

        /// <summary>
        /// Updates an account.
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="accountPutDTO">New account data</param>
        /// <returns></returns>
        /// <response code="200">Account was updated successfully</response>
        /// <response code="400">Account was not updated successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Account is not unique</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, AccountPutDTO accountPutDTO)
        {
            AccountPutDtoValidator validator = new AccountPutDtoValidator();
            ValidationResult results = validator.Validate(accountPutDTO);

            if (!results.IsValid)
                return BadRequest(results.ToString());

            try
            {
                var account = mapper.Map<Account>(accountPutDTO);
                await _accountService.UpdateAsync(account);
            }
            catch (AutoMapperMappingException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.ErrorMessage);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="accountPostDTO">New account data</param>
        /// <returns></returns>
        /// <response code="200">Account was created successfully</response>
        /// <response code="400">Account was not created successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Account is not unique</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<ActionResult<AccountFullResponseDTO>> PostAccount(AccountPostDTO accountPostDTO)
        {
            AccountPostDtoValidator validator = new AccountPostDtoValidator();
            ValidationResult results = validator.Validate(accountPostDTO);

            if (!results.IsValid)
                return BadRequest(results.ToString());

            Account accountResponse;

            try
            {
                accountResponse = await _accountService.AddAsync(mapper.Map<Account>(accountPostDTO), accountPostDTO.Password, accountPostDTO.RoleId.ToString());
            }
            catch (AutoMapperMappingException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.ErrorMessage);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.ErrorMessage);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetRole", new { id = accountResponse.Id }, mapper.Map<AccountFullResponseDTO>(accountResponse));
        }

        /// <summary>
        /// Deletes an account.
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns></returns>
        /// <response code="204">Account was deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Account was not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var success = await _accountService.RemoveByIdAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
