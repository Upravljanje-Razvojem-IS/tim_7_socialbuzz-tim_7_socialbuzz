using DeliveryService.Interface;
using DeliveryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; }

        [HttpGet("[action]")]
        public IActionResult GetAllProducts()
        {
            var result = ProductRepository.GetAll();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No products found");
        }
        [HttpGet("[action]/{id}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            var product = ProductRepository.GetById(id);
            if (product != null)
                return Ok(product);
            return NotFound();
        }

        [HttpPost("[action]")]
        public IActionResult AddProduct(Product product)
        {
           
                try
                {
                    ProductRepository.Add(product);

                    return Ok("Data inserted");
                }
                catch (ValidationException)
                {
                    return BadRequest();
                }
           
        }
        [HttpPut("[action]")]
        public IActionResult UpdateProduct(Guid id, Product product)
        {
            try
            {
                ProductRepository.Update(id, product);

                return Ok("Updated product");

            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            ProductRepository.Delete(productId);
            return Ok("Product deleted");
        }

    }
}
