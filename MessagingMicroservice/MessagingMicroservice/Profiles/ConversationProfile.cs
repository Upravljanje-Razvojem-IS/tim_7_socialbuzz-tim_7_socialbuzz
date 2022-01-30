using AutoMapper;
using MessagingMicroservice.DTOs;
using MessagingMicroservice.Models;

namespace MessagingMicroservice.Profiles
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<ConversationCreateDto, Conversation>();
            CreateMap<Conversation, ConversationReadDto>();
        }
    }
}
