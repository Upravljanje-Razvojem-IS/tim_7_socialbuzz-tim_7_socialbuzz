using AccountService.DTOs.Account;
using AccountService.Entities;
using AutoMapper;

namespace AccountService.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountPostDTO, Account>();
            CreateMap<AccountPutDTO, Account>();
            CreateMap<Account, AccountBasicResponseDTO>();
            CreateMap<Account, AccountFullResponseDTO>();
        }
    }
}
