using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Predstavlja model oglasa koji se nudi putem platforme
    /// </summary>
    public class ItemForSale
    {
        /// <summary>
        /// ID oglasa.
        /// </summary>
        public Guid itemForSaleId { get; set; }

        /// <summary>
        /// Naziv proizvoda/usluge.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Opis proizvoda/usluge.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Cena proizvoda/usluge.
        /// </summary>
        public double price { get; set; }

        public String firstName { get; set; }
        public String lastName { get; set; }
    }
}
