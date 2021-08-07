using AdMicroservice.Data.ItemForSale;
using AdMicroservice.Data.PastPrices;
using AdMicroservice.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    /// <summary>
    /// PastPrice controller with CRUD endpoints
    /// </summary>
    [ApiController]
    [Route("api/pastPrices")]
    [Produces("application/json", "application/xml")]
    public class PastPriceController : ControllerBase
    {
        private readonly IPastPriceRepository pastPriceRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public PastPriceController(IPastPriceRepository pastPriceRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.pastPriceRepository = pastPriceRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;

        }

        /// <summary>
        /// Returns list of all past prices
        /// </summary>
        /// <response code="200">Returns list of past prices</response>
        /// <response code="401">Unauthorized error</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PastPrice>> GetPastPrices()
        {
            try
            {
                var pastPrices = pastPriceRepository.GetPastPrices();
                if (pastPrices == null || pastPrices.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not found!");
                }
                return Ok(pastPrices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns all options that exist for API
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPastPriceOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
