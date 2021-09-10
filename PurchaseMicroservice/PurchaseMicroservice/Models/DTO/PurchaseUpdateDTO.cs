using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class PurchaseUpdateDTO
    {
        /// <summary>
        /// 
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
        public String Description { get; set; }
        /// <summary>
        /// 
        /// </summary>

    }
}
