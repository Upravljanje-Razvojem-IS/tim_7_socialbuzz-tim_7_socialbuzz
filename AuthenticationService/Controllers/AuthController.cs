using AuthService.Models;
using AuthService.Models.Requests;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthService.Controllers
{
    /// <summary>
    /// Controller with endpoints for authentication.
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Account login
        /// </summary>
        /// <returns>Token, if the authentication went successful</returns>
        /// <response code="200">Token</response>
        /// <response code="400">Wrong email or password</response>
        /// <response code="500">Internal server</response>
        [Route("/login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] Principal principal)
        {
            try
            {
                var authResponse = _authenticationService.Login(principal);
                if (authResponse.Result.Success)
                {
                    return Ok(new LoginSuccessResponse
                    {
                        Token = authResponse.Result.Token
                    });
                }
                return BadRequest(new AuthResponse
                {
                    Token = null,
                    Success = false,
                    Error = authResponse.Result.Error
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Account logout
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Account logged out</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server errror</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/logout")]
        [HttpPost]
        public IActionResult Logout(LogoutRequest body)
        {
            try
            {
                AuthModel authModel = _authenticationService.GetAuthModelByToken(body.Token);
                if (authModel is null)
                {
                    return BadRequest("Account with this ID logged out or does not exist");
                }
                _authenticationService.Logout(authModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
