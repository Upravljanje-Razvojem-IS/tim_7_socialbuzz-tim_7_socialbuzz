using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.DTO
{
    public class ItemForSaleDto
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
        /// Description of the item for sale
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Price of the item for sale
        /// </summary>
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the item for sale to the wall
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
