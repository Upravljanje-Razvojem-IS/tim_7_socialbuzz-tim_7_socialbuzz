using AdMicroservice.Entities;
using AdMicroservice.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ItemForSaleDto>();

            CreateMap<Service, ItemForSale>();
            CreateMap<ServiceCreationDto, Service>();
            CreateMap<Service, ServiceConfirmationDto>();

            CreateMap<ServiceUpdateDto, Service>();
        }
    }
}
