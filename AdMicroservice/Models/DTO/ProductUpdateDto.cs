﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.DTO
{
    public class ProductUpdateDto
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the product name.")]
        public String Name { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the product description.")]
        [MaxLength(300)]
        public String Description { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the product price.")]
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the product to the wall
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the AccountId.")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Weight of the product
        /// </summary>
        public String Weight { get; set; }
    }
}
