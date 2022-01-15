using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    /// <summary>
    /// Entity class which represents order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// An identifier for the order
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// the address to which it is delivered
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// quantity in the order
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// name of delivery
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// price orders
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Contact phone
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// delivery details
        /// </summary>
        public string Details { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Order(Guid id, string address, 
            int quantity, string productName, 
            double price, string contact, 
            string details, DateTime orderDate, DateTime deliveryDate)
        {
            Id = id;
            Address = address;
            Quantity = quantity;
            ProductName = productName;
            Price = price;
            Contact = contact;
            Details = details;
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
        }

        public Order()
        {
        }

        public BaseUserModel User { get; set; }
        public Product Product { get; set; }



    }
}
