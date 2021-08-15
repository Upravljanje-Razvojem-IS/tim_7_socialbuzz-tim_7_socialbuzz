using AutoMapper;
using MediaMicroservice.Entities;
using MediaMicroservice.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Profiles
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {

            CreateMap<MediaCreationDto, Media>();
            CreateMap<Media, MediaConfirmationDto>();
            CreateMap<MediaUpdateDto, Media>();

        }
    }
}
