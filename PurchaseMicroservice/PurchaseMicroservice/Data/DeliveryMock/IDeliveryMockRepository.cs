using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.DeliveryMock
{
    public interface IDeliveryMockRepository
    {
        DeliveryDTO GetDeliveryByID(Guid id);
        DeliveryDTO GetDeliveryByAdress(string adress);
    }
}
