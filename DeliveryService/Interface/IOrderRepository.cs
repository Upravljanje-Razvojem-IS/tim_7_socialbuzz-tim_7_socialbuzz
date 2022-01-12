using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IOrderRepository
    {
        List<BaseUserModel> GetAll();
        BaseUserModel GetById(Guid id);
        BaseUserModel Add();
        BaseUserModel Update(Guid id, BaseUserModel user);
        void Delete(Guid id);

    }
}
