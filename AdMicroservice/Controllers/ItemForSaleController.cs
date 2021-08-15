using AdMicroservice.Data.AccountMock;
using AdMicroservice.Data.ItemForSale;
using AdMicroservice.Entities;
using AdMicroservice.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    /// <summary>
    /// ItemForSale controller which shows all items for sale 
    /// </summary>
    [ApiController]
    [Route("api/itemsForSale")]
    [Produces("application/json", "application/xml")]
    public class ItemForSaleController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IAccountMockRepository accountMockRepository;
        private readonly IMapper mapper;

        public ItemForSaleController(IProductRepository productRepository, IServiceRepository serviceRepository, IAccountMockRepository accountMockRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.accountMockRepository = accountMockRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all items for sale
        /// </summary>
        /// <param name="firstName">First name of the user who added item</param>
        /// <returns>List of items for sale</returns>
        /// <remarks>
        /// Example of request \
        /// GET 'https://localhost:44349/api/itemsForSale'
        /// </remarks>
        /// <response code="200">Success answer - return items</response>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [AllowAnonymous] //svi korisnici mogu da pristupe metodi (GET je bezbedna i idempotentna metoda), tj. mogu da izlistaju sve oglase
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ItemForSaleDto>> GetItemForSales(string firstName)
        {
            try
            {
                List<Product> products = new List<Product>();
                List<Service> services = new List<Service>();
                if (!string.IsNullOrEmpty(firstName))
                {
                    var user = accountMockRepository.GetAccountByFirstName(firstName);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "User does not exist! Please check first name.");
                    }
                    else
                    {
                        //vracamo items for sale koje je neki user dodao
                        products = productRepository.GetProductsByAccountId(user.AccountId);
                        services = serviceRepository.GetServicesByAccountId(user.AccountId);
                    }
                }
                else
                {
                    //vracamo sve items for sale
                    products = productRepository.GetProducts();
                    services = serviceRepository.GetServices();
                }
                List<ItemForSaleDto> itemForSale = new List<ItemForSaleDto>();
                itemForSale.AddRange(mapper.Map<List<ItemForSaleDto>>(products));
                itemForSale.AddRange(mapper.Map<List<ItemForSaleDto>>(services));
                if (itemForSale.Count == 0 || itemForSale==null)
                {
                    return NoContent();
                }
                return Ok(itemForSale);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns options for working with items for sale
        /// </summary>
        /// <returns>Options for a given URL</returns>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44349/api/itemsForSale'
        /// </remarks>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetItemForSalesOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        /// <summary>
        /// Returns item for sale by id
        /// </summary>
        /// <param name="itemForSaleId">Id of item for sale</param>
        /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44349/api/itemsForSale/' \
        ///     --param  'itemForSaleId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae'
        /// </remarks>
        ///<response code="200">Success answer - return item by id</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpGet("{itemForSaleId}")]
        public ActionResult<ItemForSaleDto> GetItemForSaleById(Guid itemForSaleId)
        {
            try
            {
                var product = productRepository.GetProductById(itemForSaleId);
                if (product != null)
                {
                    return Ok(mapper.Map<ItemForSaleDto>(product));
                }

                var service = serviceRepository.GetServiceById(itemForSaleId);
                if (service != null)
                {
                    return Ok(mapper.Map<ItemForSaleDto>(service));
                }
                return StatusCode(StatusCodes.Status404NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
