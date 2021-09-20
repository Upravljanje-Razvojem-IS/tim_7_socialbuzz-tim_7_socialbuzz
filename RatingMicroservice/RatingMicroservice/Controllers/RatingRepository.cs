using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatingMicroservice.DTOs;
using RatingMicroservice.Interfaces;
using System;

namespace RatingMicroservice.Controllers
{
    [ApiController]
    [Route("api/rating")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _repository;

        public RatingController(IRatingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all ratings
        /// </summary>
        /// <returns>List of ratings</returns>
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
        /// Rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Rating</returns>
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
        /// Create Rating
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new Rating</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostCoorporate([FromBody] RatingCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update Rating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>New Rating</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, RatingCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete Rating
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
        /// Get all ratings for the item
        /// </summary>
        /// <returns>List of ratings</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{itemId}")]
        public ActionResult GetAllForTheItem(int itemId)
        {
            var entities = _repository.GetAllRatingForItem(itemId);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get all ratings for the user
        /// </summary>
        /// <returns>List of ratings</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{userId}")]
        public ActionResult GetAllOfTheUser(int userId)
        {
            var entities = _repository.GetAllRatingsOfTheUser(userId);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get all ratings for the item on specific date
        /// </summary>
        /// <returns>List of ratings</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAllForTheItemOnTheSpecificDate([FromQuery] int itemId, [FromQuery] DateTime date)
        {
            var entities = _repository.GetRatingsOfItemForSpecificDate(itemId, date);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }
    }
}
