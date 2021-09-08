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
        /// <param name="name">First name of the user who added purchase</param>
        /// <returns>List of media</returns>
        /// <response code="200">Success answer</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous] //svi korisnici mogu da pristupe metodi (GET je bezbedna i idempotentna metoda)
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
                        //vracamo media koje je neki user dodao
                        purchases = purchaseRepository.GetPurchaseByAccountId(user.AccountId);
                    }
                }
                else
                {
                    //vracamo sve media
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


    }
}
