using AdMicroservice.Entities;
using AdMicroservice.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class PastPriceProfile : Profile
    {
        public PastPriceProfile()
        {
            CreateMap<PastPriceDto, PastPrice>();
            CreateMap<PastPriceCreationDto, PastPrice>();
        }
    }
}
