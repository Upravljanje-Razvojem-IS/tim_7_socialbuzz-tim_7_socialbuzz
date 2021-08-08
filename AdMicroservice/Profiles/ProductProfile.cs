using AdMicroservice.Entities;
using AdMicroservice.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ItemForSaleDto>();

            CreateMap<Product, ItemForSale>();
            CreateMap<ProductCreationDto, Product>();
            CreateMap<Product, ProductConfirmationDto>();

            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
