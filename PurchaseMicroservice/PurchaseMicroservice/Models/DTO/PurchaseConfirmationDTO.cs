﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class PurchaseConfirmationDTO
    {

        /// <summary>
        /// An identifier for the purchase
        /// </summary>
        [Required(ErrorMessage = "You need to enter id of the purchase!")]
        public Guid PurchaseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter date of the purchase!")]
        public String Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid DeliveryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ItemForSaleId { get; set; }
    }
}
