using AccountService.DTOs.AccRole;
using AccountService.Entities;
using AutoMapper;

namespace AccountService.Profiles
{
    public class AccRoleProfile : Profile
    {
        public AccRoleProfile()
        {
            CreateMap<AccRolePostDTO, AccRole>();
            CreateMap<AccRolePutDTO, AccRole>();
            CreateMap<AccRole, AccRoleResponseDTO>();
        }
    }
}
