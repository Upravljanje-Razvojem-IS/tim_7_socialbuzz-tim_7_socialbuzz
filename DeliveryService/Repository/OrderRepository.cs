using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderRepository orderRepository;

        public BaseUserModel Add()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<BaseUserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public BaseUserModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public BaseUserModel Update(Guid id, BaseUserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
