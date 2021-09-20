using WatchingService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WatchingService.DTOs.WatchDTOs;

namespace WatchingService.Controllers
{
    [ApiController]
    [Route("api/Watch")]
    [Authorize]
    public class WatchController : ControllerBase
    {
        private readonly IWatchRepository _repository;

        public WatchController(IWatchRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all Watchs
        /// </summary>
        /// <returns>List of Watchs</returns>
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
        /// Watch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Watch</returns>
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
        /// Create Watch
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new Watch</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] WatchCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update Watch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>New Watch</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, WatchCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete Watch
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
        /// Get all Watched for user
        /// </summary>
        /// <returns>List of Watchs</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("watched/{id}")]
        public ActionResult GetWatchedForUser(int id)
        {
            var entities = _repository.GetWatchedForUser(id);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get all Watchs who Watched user
        /// </summary>
        /// <returns>List of Watchs</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("who-watch/{id}")]
        public ActionResult GetWhoWatchedUser(int id)
        {
            var entities = _repository.GetWhoWatchedUser(id);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Check if two users watch eatch outher
        /// </summary>
        /// <returns>Status</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("watch-each-other")]
        public ActionResult CheckIfTwoUsersWatchEachOther([FromQuery] int firstUser, [FromQuery] int secondUser)
        {
            var status = _repository.CheckIfUserWatchOtherUser(firstUser, secondUser);

            return Ok(new { Status = status  });
        }
    }
}
