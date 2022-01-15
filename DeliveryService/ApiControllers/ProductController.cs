using DeliveryService.DTOs.ProductDTOs;
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
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; }

        /// <summary>
        /// get all product
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            var product = ProductRepository.GetById(id);
            if (product != null)
                return Ok(product);
            return NotFound();
        }

        /// <summary>
        /// create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddProduct(ProductCreateDTO product)
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
        /// <summary>
        /// update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public IActionResult UpdateProduct(Guid id, ProductDTO product)
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
        /// <summary>
        /// delete product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            ProductRepository.Delete(productId);
            return Ok("Product deleted");
        }

    }
}
