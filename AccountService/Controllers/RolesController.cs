using AccountService.DTOs.AccRole;
using AccountService.Entities;
using AccountService.Exceptions;
using AccountService.Services.Roles;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService rolesService; 
        private readonly IMapper mapper;

        public RolesController(IRolesService rolesService, IMapper mapper)
        {
            this.rolesService = rolesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all roles.
        /// </summary>
        /// <returns>A role</returns>
        /// <response code="200">Roles array</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public ActionResult<IEnumerable<AccRoleResponseDTO>> GetRoles()
        {
            return Ok(mapper.Map<IEnumerable<AccRoleResponseDTO>>(rolesService.GetAllAsync()));
        }

        /// <summary>
        /// Returns a role.
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns>A role</returns>
        /// <response code="200">Role data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Role was not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccRoleResponseDTO>> GetRole(Guid id)
        {
            AccRole role;

            try
            {
                role = await rolesService.GetByIdAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(mapper.Map<AccRoleResponseDTO>(role));
        }

        /// <summary>
        /// Updates a role.
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <param name="role">New role data</param>
        /// <returns></returns>
        /// <response code="200">Role was updated successfully</response>
        /// <response code="400">Role was not updated successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Role is not unique</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, AccRolePutDTO rolePutDto)
        {
            try
            {
                var role = mapper.Map<AccRole>(rolePutDto);
                await rolesService.UpdateAsync(role);
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
        /// Creates a role.
        /// </summary>
        /// <param name="role">New role data</param>
        /// <returns></returns>
        /// <response code="200">Role was created successfully</response>
        /// <response code="400">Role was not created successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Role is not unique</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<ActionResult<AccRoleResponseDTO>> PostRole(AccRolePostDTO role)
        {
            AccRole roleResponse;

            try
            {
                roleResponse = await rolesService.AddAsync(mapper.Map<AccRole>(role));
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

            return CreatedAtAction("GetRole", new { id = role.Id }, mapper.Map<AccRoleResponseDTO>(roleResponse));
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns></returns>
        /// <response code="204">Role was deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Role was not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            try
            {
                var success = await rolesService.RemoveByIdAsync(id);
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
