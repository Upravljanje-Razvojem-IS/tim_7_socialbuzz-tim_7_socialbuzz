using BlockingMicroservice.DTOs.BlockDTOs;
using BlockingMicroservice.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlockingMicroservice.Controllers
{
    [ApiController]
    [Route("api/Block")]
    [Authorize]
    public class BlockController : ControllerBase
    {
        private readonly IBlockRepository _repository;

        public BlockController(IBlockRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all blocks
        /// </summary>
        /// <returns>List of Blocks</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _repository.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Block by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Block</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Create block
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new Block</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostCoorporate([FromBody] BlockCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update Block
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>New Block</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, BlockCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete block
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Get all blocked for user
        /// </summary>
        /// <returns>List of blocks</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("blocked/{id}")]
        public ActionResult GetBlocedForUser(int id)
        {
            var entities = _repository.GetBlockedForUser(id);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get all blocks who blocked user
        /// </summary>
        /// <returns>List of blocks</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("who-blocked/{id}")]
        public ActionResult GetWhoBlockedUser(int id)
        {
            var entities = _repository.GetWhoBlockedUser(id);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }
    }
}
