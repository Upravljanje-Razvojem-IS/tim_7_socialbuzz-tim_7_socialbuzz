using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Interface;
using DeliveryService.Models;
using DeliveryService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.ApiControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelContoller : ControllerBase
    {
        

        public IBaseUserModelRepository UserModelRepository { get; }

        public UserModelContoller(IBaseUserModelRepository userModelRepository)
        {
            UserModelRepository = userModelRepository;
        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllUsers()
        {
            var result = UserModelRepository.GetAll();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No users found");
        }
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult<BaseUserModel> GetUsersById(Guid id)
        {
            var user = UserModelRepository.GetById(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
        /// <summary>
        /// create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddUser(UserCreateDTO user)
        {

            try
            {
                UserModelRepository.Add(user);

                return Ok("Data inserted");
            }
            catch (ValidationException)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public IActionResult UpdateUser(Guid id, UserDTO user)
        {
            try
            {
                UserModelRepository.Update(id, user);

                return Ok("Updated user");

            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteUser(Guid userId)
        {
            UserModelRepository.Delete(userId);
            return Ok("User deleted");
        }
    }
}
