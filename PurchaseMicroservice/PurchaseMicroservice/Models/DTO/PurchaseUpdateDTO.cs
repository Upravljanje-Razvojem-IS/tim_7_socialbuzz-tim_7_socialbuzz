using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.DTO
{
    public class PurchaseUpdateDTO
    {
        [Required(ErrorMessage = "You need to enter id of the purchase!")]
        public Guid PurchaseId { get; set; }
        [Required(ErrorMessage = "It is mandatory to enter date of the purchase!")]
        public String Date { get; set; }
        public String Description { get; set; }
        public Guid AccountId { get; set; }
        public Guid DeliveryId { get; set; }
        public Guid ItemForSaleId { get; set; }
    }
}
