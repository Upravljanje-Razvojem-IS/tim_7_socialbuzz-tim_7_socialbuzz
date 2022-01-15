using AutoMapper;
using DeliveryService.DTOs.ProductDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductConfirmDTO>();
            CreateMap<Product, ProductCreateDTO>();
            CreateMap<ProductDTO, ProductConfirmDTO>();
        }
    }
}
