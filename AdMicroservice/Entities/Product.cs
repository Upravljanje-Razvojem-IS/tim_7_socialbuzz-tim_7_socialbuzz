using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Entity class which represents product
    /// </summary>
    public class Product : ItemForSale
    {
        /// <summary>
        /// Weight of the product
        /// </summary>
        public String Weight { get; set; }
    }
}
