using MessagingMicroservice.DTOs;

namespace MessagingMicroservice.Interfaces
{
    public interface IMessageRepository
    {
        List<MessageReadDto> Get();
        List<MessageReadDto> GetByUserInConversation(int userId, int conversationId);
        MessageReadDto Get(int id);
        MessageReadDto Create(MessageCreateDto dto);
        MessageReadDto Update(int id, MessageCreateDto dto);
        void Delete(int id);
        MessageReadDto ReadMessage(int id);
    }
}
