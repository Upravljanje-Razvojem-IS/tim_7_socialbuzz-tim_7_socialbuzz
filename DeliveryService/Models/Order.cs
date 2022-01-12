using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Contact { get; set; }
        public string Details { get; set; }


        public BaseUserModel User { get; set; }
        public Product Product { get; set; }



    }
}
