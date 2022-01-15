using DeliveryService.DTOs.OrderDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IOrderRepository
    {
        List<OrderDTO> GetAll();
        OrderDTO GetById(Guid id);
        OrderDTO Add(OrderCreateDTO order);
        OrderConfirmDTO Update(Guid id, OrderDTO order);
        void Delete(Guid id);

    }
}
