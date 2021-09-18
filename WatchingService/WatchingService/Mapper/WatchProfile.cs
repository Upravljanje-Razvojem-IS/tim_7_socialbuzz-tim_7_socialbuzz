using AutoMapper;
using WatchingService.DTOs.WatchDTOs;
using WatchingService.Entities;

namespace WatchingService.Mapper
{
    public class WatchProfile : Profile
    {
        public WatchProfile()
        {
            CreateMap<Watch, WatchReadDto>();
            CreateMap<Watch, WatchConfirmationDto>();
        }
    }
}
