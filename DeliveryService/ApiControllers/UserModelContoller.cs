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
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelContoller : ControllerBase
    {
        

        public IBaseUserModelRepository UserModelRepository { get; }

        public UserModelContoller(IBaseUserModelRepository userModelRepository)
        {
            UserModelRepository = userModelRepository;
        }

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
        [HttpGet("[action]/{id}")]
        public ActionResult<BaseUserModel> GetUsersById(Guid id)
        {
            var user = UserModelRepository.GetById(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
        [HttpPost("[action]")]
        public IActionResult AddUser(BaseUserModel user)
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
        [HttpPut("[action]")]
        public IActionResult UpdateUser(Guid id, BaseUserModel user)
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
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteUser(Guid userId)
        {
            UserModelRepository.Delete(userId);
            return Ok("User deleted");
        }
    }
}
