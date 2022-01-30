using MessagingMicroservice.DTOs;

namespace MessagingMicroservice.Interfaces
{
    public interface IConversationRepository
    {
        List<ConversationReadDto> Get();
        List<ConversationReadDto> GetAllForUser(int userId);
        ConversationReadDto Get(int id);
        ConversationReadDto Create(ConversationCreateDto dto);
        ConversationReadDto Update(int id, ConversationCreateDto dto);
        void Delete(int id);
        ConversationReadDto Deactivate(int id);
    }
}
