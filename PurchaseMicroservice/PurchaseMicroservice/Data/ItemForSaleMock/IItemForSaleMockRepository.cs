using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.ItemForSaleMock
{
    /// <summary>
    /// 
    /// </summary>
    public interface IItemForSaleMockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ItemForSaleDTO GetItemForSaleByID(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ItemForSaleDTO GetItemForSaleByName(string name);
    }
}
