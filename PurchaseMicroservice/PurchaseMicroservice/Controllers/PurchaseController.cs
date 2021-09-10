using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PurchaseMicroservice.Data.AccountMock;
using PurchaseMicroservice.Data.DeliveryMock;
using PurchaseMicroservice.Data.ItemForSaleMock;
using PurchaseMicroservice.Data.Purchases;
using PurchaseMicroservice.Entities;
using PurchaseMicroservice.Helpers;
using PurchaseMicroservice.Logger;
using PurchaseMicroservice.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PurchaseMicroservice.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/purchases")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IItemForSaleMockRepository itemForSaleMockRepository;
        private readonly IAccountMockRepository accountMockRepository;
        private readonly IDeliveryMockRepository deliveryMockRepository;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        private readonly IAuth auth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseRepository"></param>
        /// <param name="itemForSaleMockRepository"></param>
        /// <param name="accountMockRepository"></param>
        /// <param name="deliveryMockRepository"></param>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="auth"></param>
        public PurchaseController(IPurchaseRepository purchaseRepository, IItemForSaleMockRepository itemForSaleMockRepository,
           IAccountMockRepository accountMockRepository, IDeliveryMockRepository deliveryMockRepository, ILoggerMockRepository logger, IHttpContextAccessor contextAccessor,
           IMapper mapper, LinkGenerator linkGenerator, IAuth auth)
        {
            this.purchaseRepository = purchaseRepository;
            this.itemForSaleMockRepository = itemForSaleMockRepository;
            this.accountMockRepository = accountMockRepository;
            this.deliveryMockRepository = deliveryMockRepository;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.auth = auth;
        }

        /// <summary>
        /// Returns list of all purchases
        /// </summary>
        /// <param name="name">First name of the user who done purchase</param>
        /// <returns>List of media</returns>
        /// <response code="200">Success answer</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Purchase>> GetPurchase(string name)
        {
            try
            {

                List<Purchase> purchases;
                if (!string.IsNullOrEmpty(name))
                {
                    var user = accountMockRepository.GetAccountByFirstName(name);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "User does not exist! Please check first name.");
                    }
                    else
                    {

                        purchases = purchaseRepository.GetPurchaseByAccountId(user.AccountId);
                    }
                }
                else
                {
                    //vracamo sve purchases
                    purchases = purchaseRepository.GetAllPurchase();
                }
                if (purchases.Count == 0)
                {
                    return NoContent();
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get purchase", null);
                return Ok(purchases);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns purchase by ID
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <remarks>     
        /// EXAMPLE \
        /// purchaseId: 36e745c1-8615-4244-bf16-492b6493602f
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet("{purchaseId}")]
        public ActionResult<Purchase> GetPurchaseById(Guid purchaseId)
        {
            try
            {
                var purchase = purchaseRepository.GetPurchaseById(purchaseId);
                if (purchase == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not found");
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get purchase by id", null);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Returns purchase by itemForSaleId
        /// </summary>
        /// <param name="itemForSaleId">Id of item for sale</param>
        /// <param name="key">Authorization Key Value</param>
        /// <returns></returns>
        /// <remarks>        
        /// EXAMPLE: \
        /// KEY: Bearer Sanja \
        /// itemForSaleId = 915510b2-74fb-44b7-b265-730ac0079a0d
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
        public ActionResult<List<Purchase>> GetPurchaseByItemForSaleId(Guid itemForSaleId, [FromHeader] string key)
        {
            try
            {
                //just autorized users can access
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var purchase = purchaseRepository.GetPurchaseByItemForSaleId(itemForSaleId);
                if (purchase == null || purchase.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no purchases!");
                }
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get purchases by itemForSaleId", null);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        /// <summary>
        /// Create new purchase 
        /// </summary>
        /// <param name="purchaseCreationDTO">Model for creating purchase</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>        
        /// KEY: Bearer Sanja
        /// </remarks>
        /// <response code="201">Created purchase</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<Purchase> PostPurchase([FromBody] PurchaseCreationDTO purchaseCreationDTO, [FromHeader] string key)
        {
            try
            {
                //just autorized users can access
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }


                Purchase purchase = mapper.Map<Purchase>(purchaseCreationDTO);


                purchaseRepository.CreatePurchase(purchase);
                purchaseRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Create new purchase", null);

                string location = linkGenerator.GetPathByAction("GetPurchaseById", "Purchase", new { purchaseId = purchase.PurchaseId });
                return Created(location, purchase);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while creating purchase", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete purchase
        /// </summary>
        /// <param name="purchaseId">Purchase Id</param>
        /// <param name="key">Authorization Key Value</param>
        /// <returns></returns>
        /// <remarks>      
        /// key: Bearer Sanja
        /// </remarks>
        /// <response code="204">Deleted purchase</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="404">Not found purchase</response>
        /// <response code="500">An error on the server</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{purchaseId}")]
        public IActionResult DeletePurchase(Guid purchaseId,[FromHeader] string key)
        {

            //pristup metodi imaju samo autorizovani korisnici
            if (!auth.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
            }

            var purchase = purchaseRepository.GetPurchaseById(purchaseId);


            if (purchase == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no purchase with that ID!");
            }
            try
            {
                purchaseRepository.DeletePurchase(purchaseId);
                purchaseRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Successfully deleted purchase", null);

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while deleting purchase", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update purchase
        /// </summary>
        /// <param name="purchaseUpdateDTO">Model of purchase for update</param>
        /// <param name="purchaseId">Purchase id</param>
        /// <param name="key">Authorization Key Value</param>
         /// <remarks>        
        /// KEY: Bearer Sanja
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Updated purchase</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Forbiden request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{purchaseId}")]
        public ActionResult<Purchase> UpdatePurchase([FromBody] PurchaseUpdateDTO purchaseUpdateDTO, Guid purchaseId, [FromHeader] string key)
        {
            try
            {
                //pristup metodi imaju samo autorizovani korisnici
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var oldPurchase = purchaseRepository.GetPurchaseById(purchaseId);
                if (oldPurchase == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is no purchase with that ID!");
                }
                Purchase newPurchase = mapper.Map<Purchase>(purchaseUpdateDTO);
                //newPurchase.ItemForSaleId = oldPurchase.ItemForSaleId;

                purchaseRepository.UpdatePurchase(oldPurchase, newPurchase);
                purchaseRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Purchase updated.", null);

                return Ok(oldPurchase);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while updating purchase!", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
