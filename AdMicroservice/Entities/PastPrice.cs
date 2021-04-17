using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Predstavlja model gde se čuva prošla cena oglasa
    /// </summary>
    public class PastPrice
    {
        /// <summary>
        /// Id prošle cene.
        /// </summary>
        public Guid offerId { get; set; }

        /// <summary>
        /// Na koji oglas se odnosi prošla cena.
        /// </summary>
        public ItemForSale ItemForSale { get; set; }

        /// <summary>
        /// Iznos prošle cene.
        /// </summary>
        public double price { get; set; }
    }
}
