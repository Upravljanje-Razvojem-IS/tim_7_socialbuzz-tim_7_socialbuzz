using AutoMapper;
using LoggerMicroservice.DTOs;
using LoggerMicroservice.Entities;

namespace LoggerMicroservice.AutoMapperProfile
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Log, LogReadDto>();
            CreateMap<Log, LogConfirmationDto>();
        }
    }
}
