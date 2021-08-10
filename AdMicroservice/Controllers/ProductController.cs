using AdMicroservice.Data.ItemForSale;
using AdMicroservice.Entities;
using AdMicroservice.Logger;
using AdMicroservice.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    /// <summary>
    /// Product controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/products")]
    [Produces("application/json", "application/xml")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        public ProductController(IProductRepository productRepository, IMapper mapper, ILoggerMockRepository logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Returns list of all products
        /// </summary>
        /// <param name="pName">Name of the product</param>
        /// <returns>List of products</returns>
        /// <remarks> 
        /// GET 'https://localhost:44349/api/products' \
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<List<Product>> GetProducts(string pName)
        {

            var products = productRepository.GetProducts(pName);
            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get all products"), null);
            return Ok(mapper.Map<List<ProductConfirmationDto>>(products));

        }

        /// <summary>
        /// Returns product with specific productId
        /// </summary>
        /// <param name="productId">Id of one product</param>
        /// <remarks>        
        /// GET 'https://localhost:44349/api/products/' \
        ///     --param  'productId = 4f29d0a1-a000-4b56-9005-1a40ffcea3ae'
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(Guid productId)
        {

            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get product by id"), null);
            return Ok(mapper.Map<ProductConfirmationDto>(product));

        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Model of product</param>
        /// <remarks>
        /// POST 'https://localhost:44349/api/products/'\
        /// Example: \
        /// {
        ///        "name": "Test", \
        ///       "description": "Test description", \
        ///        "price": "100000.00 RSD", \
        ///        "accountId": "f2d8362a-124f-41a9-a22b-6e35b3a2953c", \
        ///        "weight": "500g" \
        /// }
        /// </remarks>
        /// <response code="201">Returns the created product</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">There was an error on the server</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<ProductConfirmationDto> CreateProduct([FromBody] ProductCreationDto productDto)
        {
            try
            {
                Product product = mapper.Map<Product>(productDto);
                productRepository.CreateProduct(product);
                productRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("New product created"), null);

                string location = linkGenerator.GetPathByAction("GetProductById", "Product", new { productId = product.ItemForSaleId });

                return Created(location, mapper.Map<ProductConfirmationDto>(product));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("There is error while creating product"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="productUpdateDto">Model of product</param>
        /// <param name="accountId">ID of the user who sends the request</param>
        /// <remarks>
        /// PUT 'https://localhost:44349/api/products/'\
        ///  --param  'productId = 3ca21d04-26fd-494d-a1fc-08d95c4724a9' \
        ///  --header 'accountId = f2d8362a-124f-41a9-a22b-6e35b3a2953c' \
        /// Example: \
        /// {
        ///        "name": "Update Test", \
        ///       "description": "Update Test description", \
        ///        "price": "100000.00 RSD", \
        ///        "accountId": "f2d8362a-124f-41a9-a22b-6e35b3a2953c", \
        ///        "weight": "500g" \
        /// }
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error/response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{productId}")]
        public ActionResult<Product> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto, Guid productId, [FromHeader] Guid accountId)
        {
            try
            {
                if (productUpdateDto.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("Not allowed!"));
                }

                var oldProduct = productRepository.GetProductById(productId);
                if (oldProduct == null)
                {
                    return NotFound();
                }
                Product newProduct = mapper.Map<Product>(productUpdateDto);

                productRepository.UpdateProduct(oldProduct, newProduct);

                productRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Product updated"), null);

                return Ok(oldProduct);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Update error"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId">Id of product/param>
        /// <param name="accountId">Id of the user who sends the request</param>
        /// <remarks>
        /// DELETE 'https://localhost:44349/api/products/'\
        ///  --param  'productId = 3ca21d04-26fd-494d-a1fc-08d95c4724a9' \
        ///  --header 'accountId = f2d8362a-124f-41a9-a22b-6e35b3a2953c' \
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error/response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(Guid productId, [FromHeader] Guid accountId)
        {
            try
            {
                var product = productRepository.GetProductById(productId);

                if (product.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("Not allowed!"));
                }

                if (product == null)
                {
                    return NotFound();
                }

                productRepository.DeleteProduct(productId);
                productRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Delete error"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product!");
            }
        }

        /// <summary>
        /// Returns implemented options for working with product
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// OPTIONS 'https://localhost:44349/api/products'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetProductOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
