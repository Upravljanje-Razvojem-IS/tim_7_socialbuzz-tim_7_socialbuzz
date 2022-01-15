using AutoMapper;
using DeliveryService.DTOs.OrderDTOs;
using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<Order, OrderConfirmDTO>();
            CreateMap<Order, OrderCreateDTO>();
            CreateMap<OrderDTO, OrderConfirmDTO>();
        }
    }
}
