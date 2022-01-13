﻿using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(Guid id);
        Order Add(Order order);
        Order Update(Guid id, Order order);
        void Delete(Guid id);

    }
}
