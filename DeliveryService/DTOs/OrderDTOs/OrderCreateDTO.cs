using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public BaseUserModel BuyerID { get; set; }
        public Guid SellerID { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Contact { get; set; }
        public string Details { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
