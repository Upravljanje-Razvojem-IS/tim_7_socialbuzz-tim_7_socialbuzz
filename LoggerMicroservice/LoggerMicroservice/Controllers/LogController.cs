using LoggerMicroservice.DTOs;
using LoggerMicroservice.Interfaces;
using LoggerMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoggerMicroservice.Controllers
{
    [ApiController]
    [Route("api/log")]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _repository;

        public LogController(ILogRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <returns>List of logs</returns>
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
        /// Log by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Log</returns>
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
        /// Create Log
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>new Log</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Post([FromBody] LogCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update Log
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>New Log</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, LogCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete Log
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
        /// Get all logs for date
        /// </summary>
        /// <returns>List of logs</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAllForTheDate([FromBody] DateTime date)
        {
            var list = _repository.GetAllLogsForDate(date);

            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }

        /// <summary>
        /// Get all logs between 2 dates
        /// </summary>
        /// <returns>List of logs</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAllBetweenTwoDates([FromBody] BetweenDates dates)
        {
            var list = _repository.GetAllLogsBetweenTwoDates(dates);

            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns>List of logs</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult Search([FromBody] string text)
        {
            var list = _repository.SearchLogs(text);

            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }
    }
}
