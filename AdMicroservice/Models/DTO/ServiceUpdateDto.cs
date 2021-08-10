﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.DTO
{
    public class ServiceUpdateDto
    {

        /// <summary>
        /// An identifier for the service
        /// </summary>
        public Guid ItemForSaleId { get; set; }

        /// <summary>
        /// Name of the service
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the service name.")]
        public String Name { get; set; }

        /// <summary>
        /// Description of the service
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the service description.")]
        [MaxLength(300)]
        public String Description { get; set; }

        /// <summary>
        /// Price of the service
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the service price.")]
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the service to the wall
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the AccountId.")]
        public Guid AccountId { get; set; }
    }
}
