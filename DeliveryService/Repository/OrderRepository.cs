using DeliveryService.Database;
using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderRepository order;
        private readonly DatabaseContext Context;
        public OrderRepository(DatabaseContext context) 
        {
            Context = context;
        }

        public Order Add(Order order)
        {
            Context.Add(order);
            Context.SaveChanges();
            return order;
        }

        public void Delete(Guid orderId)
        {
            var order = Context.Orders.Where(e => e.Id == orderId).FirstOrDefault();
            if (order == null)
                throw new ArgumentNullException("Order");
            else
            {
                Context.Remove(order);
                Context.SaveChanges();
            }
        
        }

        public List<Order> GetAll()
        {
            var orders = Context.Orders.Select(c => new Order()
            {
                Id = c.Id,
                Address = c.Address,
                Quantity= c.Quantity,
                ProductName =c.ProductName,
                Price = c.Price,
                Contact = c.Contact,
                Details = c.Details
            }).ToList();

            return orders;
        }

        public Order GetById(Guid id)
        {
            Order order = null;

            order = Context.Orders.Where(e => e.Id == id).Select(c => new Order()
            {
                Id = c.Id,
                Address = c.Address,
                Quantity = c.Quantity,
                ProductName = c.ProductName,
                Price = c.Price,
                Contact = c.Contact,
                Details = c.Details
            }).FirstOrDefault();
            return order;
        }

        public Order Update(Guid id, Order order)
        {
            var updatedOrder = Context.Orders.FirstOrDefault(x => x.Id == order.Id);

            if (updatedOrder == null)
                throw new ArgumentNullException("Order");

                updatedOrder.Address = order.Address;
                updatedOrder.Quantity = order.Quantity;
                updatedOrder.ProductName = order.ProductName;
                updatedOrder.Price = order.Price;
                updatedOrder.Contact = order.Contact;
                updatedOrder.Details = order.Details;

            Context.SaveChanges();
            return updatedOrder;
        }
    }
}
