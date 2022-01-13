using DeliveryService.Interface;
using DeliveryService.Models;
using DeliveryService.Repository;
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
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        public IOrderRepository OrderRepository { get; }

        [HttpGet("[action]")]
        public IActionResult GetAllOrders()
        {
            var result = OrderRepository.GetAll();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No orders found");
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Order> GetOrderById(Guid id)
        {
            var order = OrderRepository.GetById(id);
            if (order != null)
                return Ok(order);
            return NotFound();
        }
        [HttpPost("[action]")]
        public IActionResult AddOrder(Order order)
        {
            try
            {
               OrderRepository.Add(order);

                return Ok("Data inserted");
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
        }
        [HttpPut("[action]")]
        public IActionResult UpdateOrder(Guid id,Order order)
        {
            try
            {
                OrderRepository.Update(id,order);

                return Ok("Updated client");

            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteOrder(Guid orderId)
        {
            OrderRepository.Delete(orderId);
            return Ok("Order deleted");
        }

    }
}
