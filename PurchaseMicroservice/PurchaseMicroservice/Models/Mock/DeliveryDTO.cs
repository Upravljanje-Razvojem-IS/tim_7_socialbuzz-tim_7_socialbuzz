using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.Mock
{
    public class DeliveryDTO
    {
        public Guid DeliveryId { get; set; }
        public String Adress { get; set; }
        public String Price { get; set; }
    }
}
