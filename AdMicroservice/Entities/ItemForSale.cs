using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Entity class which represents items for sale
    /// </summary>
    public class ItemForSale
    {
        /// <summary>
        /// An identifier for the item for sale
        /// </summary>
        [Key]
        [Required]
        public Guid ItemForSaleId { get; set; }

        /// <summary>
        /// Name of the item for sale
        /// </summary>
        [StringLength(50)]
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// Description of the item for sale
        /// </summary>
        [StringLength(300)]
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// Price of the item for sale
        /// </summary>
        [Required]
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the item for sale to the wall
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
