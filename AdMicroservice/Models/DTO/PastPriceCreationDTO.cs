using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.DTO
{
    public class PastPriceCreationDto
    {
        /// <summary>
        /// Item for sale id to which the previous price applies
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the ItemForSaleId.")]
        public Guid ItemForSaleId { get; set; }

        /// <summary>
        /// Amount of past price
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the amount of past price.")]
        public String Price { get; set; }
    }
}
