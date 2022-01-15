using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    /// <summary>
    /// Entity class which represents  products delivered in the order
    /// </summary>
    public class Product
    {
        public Product()
        {
        }

        /// <summary>
        /// an iden tifier for the product
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// name product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  quantity of product
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Short text describing the product
        /// </summary>
        public string Description { get; set; }

        public Product(Guid id, string name, 
            int quantity, double price, 
            string description)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = description;
        }

    }
}
