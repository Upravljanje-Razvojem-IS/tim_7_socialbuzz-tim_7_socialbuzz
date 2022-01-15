using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.DTOs.OrderDTOs
{
    public class OrderConfirmDTO
    {
        public Guid Id { get; set; }
        public string Contact { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}
