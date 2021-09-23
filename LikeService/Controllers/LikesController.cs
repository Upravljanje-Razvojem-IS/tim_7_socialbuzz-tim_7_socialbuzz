using LikeService.Data.Interfaces;
using LikeService.Dtos;
using LikeService.Logger;
using LikeService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LikeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILikeRepository _likeRepository;
        private readonly FakeLogger _logger;
        private readonly IAccountRepository _accountRepository;

        public LikesController(
            IMapper mapper,
            ILikeRepository likeRepository,
            FakeLogger logger,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _likeRepository = likeRepository;
            _logger = logger;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Creates a new like
        /// </summary>
        /// <response code="200">Like created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        /// <param name="likeCreateDto"></param>
        /// <returns>New Like</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LikeReadDto> Create([FromBody] LikeCreateDto likeCreateDto)
        {
            Account account = _accountRepository.Get(likeCreateDto.AccountUid);
            if (account == null)
            {
                return BadRequest("Account doesnt exist");
            }
            Like like = _mapper.Map<Like>(likeCreateDto);
            _likeRepository.Create(like);
            _logger.Log("Like created");
            return Ok(_mapper.Map<LikeReadDto>(like));
        }

        /// <summary>
        /// Returns all likes
        /// </summary>
        /// <response code="200">Likes returned</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Likes</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<LikeReadDto>> Get()
        {
            List<Like> result = _likeRepository.Get().ToList();
            _logger.Log("Get likes");
            return Ok(_mapper.Map<IEnumerable<LikeReadDto>>(result));
        }
        /// <summary>
        /// Returns an like
        /// </summary>
        /// <response code="200">Like returned</response>
        /// <response code="400">Like doesn't exist</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Like</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<LikeReadDto> Get(int id)
        {
            if (_likeRepository.Get(id) == null)
            {
                return BadRequest("Like with that Id doesn't exist.");
            }

            Like result = _likeRepository.Get(id);
            _logger.Log("Get like");
            return Ok(_mapper.Map<LikeReadDto>(result));
        }

        /// <summary>
        /// Get users like.
        /// </summary>
        /// <response code="200">Returns likes for ad.</response>
        /// <response code="400">User doesn't exist or user doesn't have an like</response>
        /// <response code="500">Internal server error</response>
        /// <param name="userId"></param>
        /// <returns>Like</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ad/{adId}")]
        public ActionResult<LikeReadDto> GetForAd(int adId)
        {
            var likes = _likeRepository.Get()
                .Where(like => like.AdId == adId).ToList();
            _logger.Log("Get likes for ad");
            return Ok(_mapper.Map<List<LikeReadDto>>(likes));
        }

        /// <summary>
        /// Updates like.
        /// </summary>
        /// <response code="200">Like updated</response>
        /// <response code="400">Like doesn't exist or bad request</response>
        /// <response code="500">Internal server error</response>
        /// <param name="likeUpdateDto"></param>
        /// <returns>Like</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<LikeReadDto> Update(LikeUpdateDto likeUpdateDto)
        {
            Like like = _likeRepository.Get(likeUpdateDto.Id);
            if (like == null)
            {
                return BadRequest("Like with that id doesn't exist.");
            }
            like = _mapper.Map(likeUpdateDto, like);
            _likeRepository.Update(like);
            _logger.Log("Update like");
            return Ok(like);
        }

        /// <summary>
        /// Deletes an like.
        /// </summary>
        /// <response code="200">Like deleted</response>
        /// <response code="400">Like doesn't exist</response>
        /// <response code="500">Internal server error</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult<LikeReadDto> Delete(int id)
        {
            Like like = _likeRepository.Get(id);
            if (like == null)
            {
                return BadRequest("Like with that id doesn't exit");
            }
            _likeRepository.Delete(like);
            _logger.Log("Delete Like");
            return Ok();
        }
    }
}
