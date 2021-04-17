using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Predstavlja model proizvoda
    /// </summary>
    public class Product : ItemForSale
    {
        /// <summary>
        /// Težina proizvoda.
        /// </summary>
        public double weight { get; set; }
    }
}
