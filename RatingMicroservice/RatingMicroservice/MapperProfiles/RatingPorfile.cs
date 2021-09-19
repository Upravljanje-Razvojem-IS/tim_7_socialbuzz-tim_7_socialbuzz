using AutoMapper;
using RatingMicroservice.DTOs;
using RatingMicroservice.Entities;

namespace RatingMicroservice.MapperProfiles
{
    public class RatingPorfile : Profile
    {
        public RatingPorfile()
        {
            CreateMap<Rating, RatingReadDto>();
            CreateMap<Rating, RatingConfirmationDto>();
        }
    }
}
