using AutoMapper;
using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BaseUserModel, UserDTO>();
            CreateMap<BaseUserModel, UserCreateDTO>();
            CreateMap<BaseUserModel, UserConfirmDTO>();
            CreateMap<BaseUserModel, UserLogInDTO>();
            CreateMap<UserDTO, UserConfirmDTO>();
            CreateMap<UserDTO, UserLogInDTO>();
            CreateMap<UserConfirmDTO, UserDTO>();
        }
    }
}
