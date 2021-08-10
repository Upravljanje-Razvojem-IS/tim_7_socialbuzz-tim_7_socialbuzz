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
    /// Service controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/services")]
    [Produces("application/json", "application/xml")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        public ServiceController(IServiceRepository serviceRepository, IMapper mapper, ILoggerMockRepository logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
        {
            this.serviceRepository = serviceRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Returns list of all services
        /// </summary>
        /// <param name="sName">Name of the service</param>
        /// <returns>List of services</returns>
        /// <remarks> 
        /// GET 'https://localhost:44349/api/services' \
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<List<Service>> GetServices(string sName)
        {

            var services = serviceRepository.GetServices(sName);

            if (services == null || services.Count == 0)
            {
                return NoContent();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get all services"), null);
            return Ok(mapper.Map<List<ServiceConfirmationDto>>(services));

        }

        /// <summary>
        /// Returns service with specific serviceId
        /// </summary>
        /// <param name="serviceId">Id of one service</param>
        /// <remarks>        
        /// GET 'https://localhost:44349/api/services/' \
        ///     --param  'serviceId = 1f4aa5b3-a67f-45c5-b519-771a7c09a944'
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{serviceId}")]
        public ActionResult<Service> GetServiceById(Guid serviceId)
        {

            var service = serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Get service by id"), null);
            return Ok(mapper.Map<ServiceConfirmationDto>(service));

        }

        /// <summary>
        /// Add new service
        /// </summary>
        /// <param name="serviceDto">Model of service</param
        /// <remarks>
        /// POST 'https://localhost:44349/api/services/'\
        /// Example: \
        /// {
        ///        "name": "Test service", \
        ///        "description": "Test description", \
        ///        "price": "5000.00 RSD", \
        ///        "accountId": "b1d1e043-85c9-4ee1-9eb7-38314c109607" \
        ///}
    /// </remarks>
    /// <response code="201">Returns the created service</response>
    /// <response code="401">Unauthorized user</response>
    /// <response code="500">There was an error on the server</response>
    [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<ServiceConfirmationDto> CreateService([FromBody] ServiceCreationDto serviceDto)
        {
            try
            {
                Service service = mapper.Map<Service>(serviceDto);
                serviceRepository.CreateService(service);
                serviceRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("New service created"), null);

                string location = linkGenerator.GetPathByAction("GetServiceById", "Service", new { serviceId = service.ItemForSaleId });

                return Created(location, mapper.Map<ServiceConfirmationDto>(service));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("There is error while creating service"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="serviceUpdateDto">Model of service</param>
        /// <param name="accountId">ID of the user who sends the request</param>
        ///         /// <remarks>
        /// PUT 'https://localhost:44349/api/products/'\
        ///  --param  'serviceId = 2228e12e-9e5f-46cf-f59e-08d95c4b3916' \
        ///  --header 'accountId = b1d1e043-85c9-4ee1-9eb7-38314c109607' \
        /// Example: \
        /// {
        ///        "name": "Update Test service", \
        ///        "description": "Update Test description", \
        ///        "price": "5000.00 RSD", \
        ///        "accountId": "b1d1e043-85c9-4ee1-9eb7-38314c109607" \
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
        [HttpPut("{serviceId}")]
        public ActionResult<Product> UpdateService([FromBody] ServiceUpdateDto serviceUpdateDto, Guid serviceId, [FromHeader] Guid accountId)
        {
            try
            {
                if (serviceUpdateDto.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("Not allowed!"));
                }

                var oldService = serviceRepository.GetServiceById(serviceId);
                if (oldService == null)
                {
                    return NotFound();
                }
                Service newService = mapper.Map<Service>(serviceUpdateDto);

                serviceRepository.UpdateService(oldService, newService);

                serviceRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Product updated"), null);

                return Ok(oldService);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Update error"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete services
        /// </summary>
        /// <param name="serviceId">Id of service/param>
        /// <param name="accountId">ID of the user who sends the request</param>
        ///         /// <remarks>
        /// DELETE 'https://localhost:44349/api/services/'\
        ///  --param  'serviceId = 2228e12e-9e5f-46cf-f59e-08d95c4b3916' \
        ///  --header 'accountId = b1d1e043-85c9-4ee1-9eb7-38314c109607' \
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
        [HttpDelete("{serviceId}")]
        public IActionResult DeleteService(Guid serviceId, [FromHeader] Guid accountId)
        {
            try
            {
                var service = serviceRepository.GetServiceById(serviceId);

                if (service.AccountId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, String.Format("Not allowed!"));
                }

                if (service == null)
                {
                    return NotFound();
                }

                serviceRepository.DeleteService(serviceId);
                serviceRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", String.Format("Delete error"), null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product!");
            }
        }

        /// <summary>
        /// Returns implemented options for working with service
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// OPTIONS 'https://localhost:44349/api/services'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetServiceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
