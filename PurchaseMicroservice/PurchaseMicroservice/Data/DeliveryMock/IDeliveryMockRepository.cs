using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.DeliveryMock
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeliveryMockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeliveryDTO GetDeliveryByID(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        DeliveryDTO GetDeliveryByAddress(string address);
    }
}
