using MessagingMicroservice.DTOs;
using MessagingMicroservice.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessagingMicroservice.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repository;

        public ConversationController(IConversationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all Conversations
        /// </summary>
        /// <returns>List of Conversations</returns>
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
        /// Get all Conversations for user
        /// </summary>
        /// <returns>List of Conversations</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{userId}")]
        public ActionResult GetAllForUser(int userId)
        {
            var entities = _repository.GetAllForUser(userId);

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get Conversation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Conversation</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Create new Conversation
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>New Conversation</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostConversation([FromBody] ConversationCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update Conversation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Updated Conversation</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult PutConversation(int id, ConversationCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete Conversation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult DeleteConversation(int id)
        {
            _repository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deactivated</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPatch("{id}")]
        public ActionResult Deactivate(int id)
        {
            var entity = _repository.Deactivate(id);

            return Ok(entity);
        }
    }
}
