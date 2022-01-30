using AutoMapper;
using MessagingMicroservice.DTOs;
using MessagingMicroservice.Models;

namespace MessagingMicroservice.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageCreateDto, Message>();
            CreateMap<Message, MessageReadDto>();
        }
    }
}
