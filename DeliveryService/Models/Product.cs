using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
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
