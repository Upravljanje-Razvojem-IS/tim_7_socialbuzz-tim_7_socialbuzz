using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.Mock
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliveryDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid DeliveryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Price { get; set; }
    }
}
