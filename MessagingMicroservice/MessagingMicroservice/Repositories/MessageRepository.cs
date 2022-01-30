using AutoMapper;
using MessagingMicroservice.CustomException;
using MessagingMicroservice.DTOs;
using MessagingMicroservice.Interfaces;
using MessagingMicroservice.MockLogger;
using MessagingMicroservice.Models;

namespace MessagingMicroservice.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IFakeLogger _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(IMapper mapper, DatabaseContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public MessageReadDto Create(MessageCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.SenderId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var message = _mapper.Map<Message>(dto);

            _context.Messages.Add(message);

            _context.SaveChanges();

            _logger.Log("Message created!");

            return _mapper.Map<MessageReadDto>(message);
        }

        public void Delete(int id)
        {
            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (message == null)
                throw new BusinessException("Message does not exist");

            _context.Messages.Remove(message);

            _context.SaveChanges();

            _logger.Log("Message deleted!");
        }

        public List<MessageReadDto> Get()
        {
            var messages = _context.Messages.ToList();

            _logger.Log("Messages fetched!");

            return _mapper.Map<List<MessageReadDto>>(messages);
        }

        public MessageReadDto Get(int id)
        {
            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (message == null)
                throw new BusinessException("Message does not exist");

            _logger.Log("Message fetched!");

            return _mapper.Map<MessageReadDto>(message);
        }

        public MessageReadDto Update(int id, MessageCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.SenderId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (message == null)
                throw new BusinessException("Message does not exist");

            message.Content = dto.Content;
            message.SenderId = dto.SenderId;
            message.ConversationId = dto.ConversationId;
            message.CreatedAt = dto.CreatedAt;
            message.UpdatedAt = dto.UpdatedAt;
            message.IsRead = dto.IsRead;

            _context.SaveChanges();

            _logger.Log("Message updated!");

            return _mapper.Map<MessageReadDto>(message);
        }

        public MessageReadDto ReadMessage(int id)
        {
            var message = _context.Messages.FirstOrDefault(e => e.Id == id);

            if (message == null)
                throw new BusinessException("Message does not exist");

            message.IsRead = true;

            _context.SaveChanges();

            _logger.Log("Message read!");

            return _mapper.Map<MessageReadDto>(message);
        }

        public List<MessageReadDto> GetByUserInConversation(int userId, int conversationId)
        {
            var messages = _context.Messages
                .Where(e => e.SenderId == userId && e.ConversationId == conversationId);

            _logger.Log("Messages fetched!");

            return _mapper.Map<List<MessageReadDto>>(messages);
        }
    }
}
