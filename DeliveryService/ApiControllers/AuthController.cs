using DeliveryService.Database;
using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Interface;
using DeliveryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.ApiControllers
{
    /// <summary>
    /// AuthController  allows the implementation of user authorization
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext Context;
        private readonly IBaseUserModelRepository UserRepository;
        private readonly IPasswordHasher PasswordHasher;
        private readonly AccessTokenGenerator AccessTokenGenerator;

        /// <summary>
        /// constructor AuthController through the passed parameters enables the implementation of authorization
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userRepository"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="accessTokenGenerator"></param>
        public AuthController(DatabaseContext context, 
            IBaseUserModelRepository userRepository, 
            IPasswordHasher passwordHasher, 
            AccessTokenGenerator accessTokenGenerator)
        {
            Context = context;
            UserRepository = userRepository;
            PasswordHasher = passwordHasher;
            AccessTokenGenerator = accessTokenGenerator;
        }

        /// <summary>
        ///  login method allows users to log in
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Login(UserLogInDTO loginUser)
        {
            var user = new UserLogInDTO(
                loginUser.Email,
                loginUser.PasswordHash
                );

            if (!ModelState.IsValid)
            {
                return BadRequest("That user does not exist!");
            }

            var userdb = Context.Users.FirstOrDefault(u => (u.Email == user.Email));
            if (userdb == null)
            {
                return Unauthorized();
            }


            bool isPwCorrect = PasswordHasher.VerifyPassword(user.PasswordHash, userdb.PasswordHash);
            if (!isPwCorrect)
            {
                return Unauthorized();
            }

            string accessToken = AccessTokenGenerator.GenerateToken(userdb);

            Response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }
        /// <summary>
        ///  logout method allows users to log out
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}
