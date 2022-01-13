using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IBaseUserModelRepository
    {
        List<BaseUserModel> GetAll();
        BaseUserModel GetById(Guid id);
        BaseUserModel Add(BaseUserModel userModel);
        BaseUserModel Update(Guid id, BaseUserModel userModel);
        void Delete(Guid id);
    }
}
