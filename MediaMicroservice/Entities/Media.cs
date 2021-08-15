using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Entities
{
    /// <summary>
    /// Entity class which represents multimedia content which are added to the items for sale
    /// </summary>
    public class Media
    {
        /// <summary>
        /// An identifier for the media
        /// </summary>
        [Key]
        [Required]
        public Guid MediaId { get; set; }

        /// <summary>
        /// The file path for the media
        /// </summary>
        [Required]
        public String FilePath { get; set; }

        /// <summary>
        /// ItemForSaleId to which the media is added
        /// </summary>
        public Guid ItemForSaleId { get; set; }

        /// <summary>
        /// Id of the user who adds media for item
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
