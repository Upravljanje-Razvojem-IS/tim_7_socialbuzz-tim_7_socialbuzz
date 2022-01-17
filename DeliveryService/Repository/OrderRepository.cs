using AutoMapper;
using DeliveryService.Database;
using DeliveryService.DTOs.OrderDTOs;
using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    /// <summary>
    /// Order repository shows all order
    /// </summary>
    public class OrderRepository : IOrderRepository
    {

        private readonly DatabaseContext Context;
        private readonly IMapper Mapper;
        public OrderRepository(DatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;

        }

        /// <summary>
        ///  add a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderDTO Add(OrderCreateDTO order)
        {
            Order addedOrder = new Order()
            {
                Id = Guid.NewGuid(),
                User = order.BuyerID,
                Address = order.Address,
                Quantity = order.Quantity,
                ProductName = order.ProductName,
                Price = order.Price,
                Contact = order.Contact,
                Details = order.Details,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate
            };

            Context.Orders.Add(addedOrder);
            Context.SaveChanges();

            return Mapper.Map<OrderDTO>(addedOrder);
        }

        /// <summary>
        /// delete order 
        /// </summary>
        /// <param name="orderId"></param>
        public void Delete(Guid orderId)
        {
            var order = Context.Orders.Where(e => e.Id == orderId).FirstOrDefault();
            if (order == null)
                throw new ArgumentNullException("Order does not exist");
            else
            {
                Context.Remove(order);
                Context.SaveChanges();
            }

        }

        /// <summary>
        /// show all orders
        /// </summary>
        /// <returns></returns>
        public List<OrderDTO> GetAll()
        {
            var orders = Context.Orders.Select(c => new Order()
            {
                Id = c.Id,
                User = c.User,
                Address = c.Address,
                Quantity = c.Quantity,
                ProductName = c.ProductName,
                Price = c.Price,
                Contact = c.Contact,
                Details = c.Details
            }).ToList();

            return Mapper.Map<List<OrderDTO>>(orders);
        }

        /// <summary>
        ///  returns order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDTO GetById(Guid id)
        {
            var order = Context.Orders.Where(e => e.Id == id).Select(c => new Order()
            {
                Id = c.Id,
                User = c.User,
                Address = c.Address,
                Quantity = c.Quantity,
                ProductName = c.ProductName,
                Price = c.Price,
                Contact = c.Contact,
                Details = c.Details
            }).FirstOrDefault();

            return Mapper.Map<OrderDTO>(order);
        }

        /// <summary>
        ///  update order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderConfirmDTO Update(Guid id, OrderDTO order)
        {
            var updatedOrder = Context.Orders.FirstOrDefault(x => x.Id == id);

            if (updatedOrder == null)
                throw new ArgumentNullException("Order");

            updatedOrder.Address = order.Address;
            updatedOrder.Quantity = order.Quantity;
            updatedOrder.ProductName = order.ProductName;
            updatedOrder.Price = order.Price;
            updatedOrder.Contact = order.Contact;
            updatedOrder.Details = order.Details;

            Context.SaveChanges();

            return Mapper.Map<OrderConfirmDTO>(updatedOrder);
        }
    }
}
