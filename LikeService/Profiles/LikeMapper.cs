using LikeService.Dtos;
using LikeService.Models;
using AutoMapper;

namespace LikeService.Profiles
{
    public class LikeMapper : Profile
    {
        public LikeMapper()
        {
            CreateMap<Like, LikeReadDto>();
            CreateMap<LikeCreateDto, Like>();
            CreateMap<LikeUpdateDto, Like>();
        }
    }
}
