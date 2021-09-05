using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Entities
{

    /// <summary>
    /// Entity class which represents purchase
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// An identifier for the purchase
        /// </summary>
        [Key]
        [Required]
        public Guid PurchaseId { get; set; }
        /// <summary>
        /// date and time of purchase
        /// </summary>
        [Required]
        public String Date { get; set; }
        /// <summary>
        /// Short text describing the purchase
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Id of the user who made the purchase
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// Id of delivery
        /// </summary>
        public Guid DeliveryId { get; set; }
        /// <summary>
        /// Id of the sold item
        /// </summary>
        public Guid ItemForSaleId { get; set; }
    }
}
