using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Models.DTO
{
    public class MediaCreationDto
    {

        /// <summary>
        /// The file path for the media
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the file path for the media.")]
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
