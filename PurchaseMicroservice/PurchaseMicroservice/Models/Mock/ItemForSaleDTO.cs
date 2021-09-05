using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.Mock
{
    public class ItemForSaleDTO
    {
        /// <summary>
        /// An identifier for the item for sale
        /// </summary>
        public Guid ItemForSaleId { get; set; }

        /// <summary>
        /// Name of the item for sale
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Price of the item for sale
        /// </summary>
        public String Price { get; set; }

    }
}
