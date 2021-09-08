using AutoMapper;
using PurchaseMicroservice.Entities;
using PurchaseMicroservice.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class PurchaseProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>

        public PurchaseProfile()
        {

            CreateMap<PurchaseCreationDTO, Purchase>();
            CreateMap<Purchase, PurchaseConfirmationDTO>();
            CreateMap<PurchaseUpdateDTO, Purchase>();

        }
    }
}
