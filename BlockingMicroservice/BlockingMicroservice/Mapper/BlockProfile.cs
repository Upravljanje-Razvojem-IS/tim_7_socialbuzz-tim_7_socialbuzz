using AutoMapper;
using BlockingMicroservice.DTOs.BlockDTOs;
using BlockingMicroservice.Entities;

namespace BlockingMicroservice.Mapper
{
    public class BlockProfile : Profile
    {
        public BlockProfile()
        {
            CreateMap<Block, BlockReadDto>();
            CreateMap<Block, BlockConfirmationDto>();
        }
    }
}
