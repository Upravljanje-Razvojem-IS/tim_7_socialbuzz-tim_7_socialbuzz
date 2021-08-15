using AutoMapper;
using MediaMicroservice.Data.AccountMock;
using MediaMicroservice.Data.Medias;
using MediaMicroservice.Entities;
using MediaMicroservice.Helpers;
using MediaMicroservice.Logger;
using MediaMicroservice.Models.DTO;
using MediaMicroservice.Models.Exceptions;
using MediaMicroservice.Models.Mock;
using MediaMicroservice.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaMicroservice.Controllers
{
    /// <summary>
    /// Media controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/medias")]
    [Produces("application/json", "application/xml")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository mediaRepository;
        private readonly IItemForSaleService itemForSaleService;
        private readonly IAccountMockRepository accountMockRepository;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        private readonly IAuthHelper auth;

        public MediaController(IMediaRepository mediaRepository, IItemForSaleService itemForSaleService, IAccountMockRepository accoutMockRepository, ILoggerMockRepository logger,
                                  IHttpContextAccessor contextAccessor, IMapper mapper, LinkGenerator linkGenerator, IAuthHelper auth)
        {
            this.mediaRepository = mediaRepository;
            this.itemForSaleService = itemForSaleService;
            this.accountMockRepository = accoutMockRepository;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.auth = auth;
        }

        /// <summary>
        /// Returns list of all media
        /// </summary>
        /// <param name="name">First name of the user who added media</param>
        /// <returns>List of media</returns>
        /// <remarks> 
        /// Example of request \
        /// GET 'https://localhost:44388/api/medias' \
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous] //svi korisnici mogu da pristupe metodi (GET je bezbedna i idempotentna metoda)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Media>> GetMedia(string name)
        {
            try
            {
                
                List<Media> media;
                if (!string.IsNullOrEmpty(name))
                {
                    var user = accountMockRepository.GetAccountByFirstName(name);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "User does not exist! Please check first name.");
                    }
                    else
                    {
                        //vracamo media koje je neki user dodao
                        media = mediaRepository.GetMediaByAccountId(user.AccountId);
                    }
                }
                else
                {
                    //vracamo sve media
                    media = mediaRepository.GetMedias();
                }
                if (media.Count == 0 || media == null)
                {
                    return NoContent();
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get media"), null);
                return Ok(media);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns media with specific mediaId
        /// </summary>
        /// <param name="mediaId">Id of one media</param>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44388/api/medias/' \
        ///     --param  'mediaId = 91cc2b07-a231-4fe7-bf3b-48821e35c904'
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet("{mediaId}")]
        public ActionResult<Media> GetMediaById(Guid mediaId)
        {
            try
            {
                var media = mediaRepository.GetMediaById(mediaId);
                if (media == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not found");
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get media by id"), null);
                return Ok(media);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Returns media by itemForSaleId
        /// </summary>
        /// <param name="itemForSaleId">Id of item for sale</param>
        /// <param name="key">Authorization Key Value</param>
        /// <returns></returns>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44388/api/medias/itemForSaleId/' \
        /// --header 'key: Bearer Bojana' \
        /// --param 'itemForSaleId = 86f5ae7c-ef07-4339-9f46-c8f597560565'
        /// </remarks>
        /// <response code="200">Returns the media</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("itemForSaleId/{itemForSaleId}")]
        public ActionResult<List<Media>> GetMediaByItemForSaleId(Guid itemForSaleId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var media = mediaRepository.GetMediaByItemForSaleId(itemForSaleId);
                if (media == null || media.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no media!");
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get media by itemForSaleId"), null);
                return Ok(media);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Create media 
        /// </summary>
        /// <param name="mediaCreationDto">Model of media to create</param>
        /// <param name="accountId">Id of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// POST 'https://localhost:44388/api/medias/' \
        ///  --header 'key: Bearer Bojana' \
        ///  --param 'accountId = 9888cf22-b353-4162-aedc-734ca2dc26a4' \
        /// { \
        ///     "filePath": "https://img.gigatron.rs/img/products/large/image5f57467f62dd8.png", \
        ///     "itemForSaleId": "2d53fc22-eac4-43bb-8f55-d2b8495603cc", \
        ///     "accountId": "9888cf22-b353-4162-aedc-734ca2dc26a4" \
        /// } 
        /// </remarks>
        /// <response code="201">Created media</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="500">There was an error on the server</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<Media> CreateMedia([FromBody] MediaCreationDto mediaCreationDto, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                Media media = mapper.Map<Media>(mediaCreationDto);

                //poziv drugog servisa
                var itemForSale = itemForSaleService.GetItemForSaleById<ItemForSaleDto>(HttpMethod.Get, media.ItemForSaleId).Result;
                if (itemForSale == null)
                {
                    throw new DbException("Item with that ID doesn't exist!");
                }

                //samo onaj ko je postavio proizvod moze da mu dodaje multimedijalni sadrzaj
                if (mediaCreationDto.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("Not allowed!"));
                }

                mediaRepository.CreateMedia(media);
                mediaRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Create new media"), null);

                string location = linkGenerator.GetPathByAction("GetMediaById", "Media", new { mediaId = media.MediaId});
                return Created(location, media);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Error while creating media", ex.Message), null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update media
        /// </summary>
        /// <param name="mediaUpdateDto">Model of media for update</param>
        /// <param name="mediaId">Media id</param>
        /// <param name="accountId">Id of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <returns></returns>
        /// <remarks>
        /// Example of successful request \
        /// PUT 'https://localhost:44388/api/medias/' \
        ///  --header 'key: Bearer Bojana' \
        ///  --param 'mediaId = 8298b502-1d16-42b8-b931-08d9600f0bdf' -> change this for testing\
        ///  --param 'accountId = 9888cf22-b353-4162-aedc-734ca2dc26a4' \
        /// { \
        ///     "filePath": "https://www.winwin.rs/media/catalog/product/151/02/151026_5e79cb2344968.png", \
        ///     "itemForSaleId": "2d53fc22-eac4-43bb-8f55-d2b8495603cc", \
        ///     "accountId": "9888cf22-b353-4162-aedc-734ca2dc26a4" \
        /// } \
        /// Example of bad request \
        /// PUT 'https://localhost:44388/api/medias/' \
        ///  --header 'key: Bearer Bojana' \
        ///  --param 'mediaId = 8298b502-1d16-42b8-b931-08d9600f0bdf' \
        ///  --param 'accountId = 42B70088-9DBD-4B19-8FC7-16414E94A8A6' \
        /// { \
        ///     "filePath": "https://www.winwin.rs/media/catalog/product/151/02/151026_5e79cb2344968.png", \
        ///     "itemForSaleId": "2d53fc22-eac4-43bb-8f55-d2b8495603cc", \
        ///     "accountId": "9888cf22-b353-4162-aedc-734ca2dc26a4" \
        /// } 
        /// </remarks>
        /// <response code="200">Updated media</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Error on the server</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{mediaId}")]
        public ActionResult<Media> UpdateMedia([FromBody] MediaUpdateDto mediaUpdateDto, Guid mediaId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var oldMedia = mediaRepository.GetMediaById(mediaId);
                if (oldMedia == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, String.Format("There is no media!"));
                }
                Media newMedia = mapper.Map<Media>(mediaUpdateDto);
                newMedia.ItemForSaleId= oldMedia.ItemForSaleId;

                if (oldMedia.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("User doesn't have permission to update media because he didn't create it"));
                }

                var itemForSale = itemForSaleService.GetItemForSaleById<ItemForSaleDto>(HttpMethod.Get, oldMedia.ItemForSaleId).Result;
                if (itemForSale == null)
                {
                    throw new DbException("Item for sale with that ID doesn't exist!");
                }

                mediaRepository.UpdateMedia(oldMedia, newMedia);
                mediaRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Updated media."), null);

                return Ok(oldMedia);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Error while updating media!", mediaId, ex.Message), null);
   
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete media
        /// </summary>
        /// <param name="mediaId">Media Id</param>
        /// <param name="accountId">Id of the user who sends the request</param>
        /// <param name="key">Authorization Key Value</param>
        /// <returns></returns>
        /// <remarks>      
        /// Example of request \
        /// DELETE 'https://localhost:44388/api/medias/' \
        ///     --header 'key: Bearer Bojana' \
        ///     --param accountId = '9888cf22-b353-4162-aedc-734ca2dc26a4' \
        ///     --param mediaId = '8298b502-1d16-42b8-b931-08d9600f0bdf' -> change this for testing 
        /// </remarks>
        /// <response code="204">Deleted media</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="404">Not found media</response>
        /// <response code="500">An error on the server</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{mediaId}")]
        public IActionResult DeleteMedia(Guid mediaId, [FromHeader] Guid accountId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var media = mediaRepository.GetMediaById(mediaId);

                if (media.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("User doesn't have permission to delete media because he didn't create it"));
                }

                if (media == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, String.Format("There is no media!"));
                }

                mediaRepository.DeleteMedia(mediaId);
                mediaRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Successfully deleted media"), null);

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Error while deleting media"), null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Return implemented options for working with media
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44388/api/medias'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous] //svi mogu da pristupe
        public IActionResult GetMediaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
