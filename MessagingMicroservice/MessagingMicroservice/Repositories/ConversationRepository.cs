using AutoMapper;
using MessagingMicroservice.CustomException;
using MessagingMicroservice.DTOs;
using MessagingMicroservice.Interfaces;
using MessagingMicroservice.MockLogger;
using MessagingMicroservice.Models;

namespace MessagingMicroservice.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly IFakeLogger _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ConversationRepository(IMapper mapper, DatabaseContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public ConversationReadDto Create(ConversationCreateDto dto)
        {
            var firstUser = UserData.Users.FirstOrDefault(e => e.Id == dto.FirstUserId);
            var secondUser = UserData.Users.FirstOrDefault(e => e.Id == dto.SecondUserId);

            if (firstUser == null || secondUser == null)
                throw new BusinessException("User does not exist", 400);

            var conversation = _mapper.Map<Conversation>(dto);

            _context.Conversations.Add(conversation);

            _context.SaveChanges();

            _logger.Log("Conversation created!");

            return _mapper.Map<ConversationReadDto>(conversation);
        }

        public ConversationReadDto Deactivate(int id)
        {
            var conversation = _context.Conversations.FirstOrDefault(e => e.Id == id);

            if (conversation == null)
                throw new BusinessException("Conversation does not exist");

            conversation.IsActive = false;

            _context.SaveChanges();

            _logger.Log("Conversation deactivated");

            return _mapper.Map<ConversationReadDto>(conversation);
        }

        public void Delete(int id)
        {
            var conversation = _context.Conversations.FirstOrDefault(e => e.Id == id);

            if (conversation == null)
                throw new BusinessException("Conversation does not exist");


            _context.Conversations.Remove(conversation);

            _context.SaveChanges();

            _logger.Log("Conversation deleted!");
        }

        public List<ConversationReadDto> Get()
        {
            var conversations = _context.Conversations.ToList();

            return _mapper.Map<List<ConversationReadDto>>(conversations);
        }

        public ConversationReadDto Get(int id)
        {
            var conversation = _context.Conversations.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<ConversationReadDto>(conversation);
        }

        public List<ConversationReadDto> GetAllForUser(int userId)
        {
            var conversations = _context.Conversations.Where(e => e.FirstUserId == userId || e.SecondUserId == userId);

            return _mapper.Map<List<ConversationReadDto>>(conversations);
        }

        public ConversationReadDto Update(int id, ConversationCreateDto dto)
        {
            var firstUser = UserData.Users.FirstOrDefault(e => e.Id == dto.FirstUserId);
            var secondUser = UserData.Users.FirstOrDefault(e => e.Id == dto.SecondUserId);

            if (firstUser == null || secondUser == null)
                throw new BusinessException("User does not exist", 400);

            var conversation = _context.Conversations.FirstOrDefault(e => e.Id == id);

            if (conversation == null)
                throw new BusinessException("conversation does not exist");

            conversation.FirstUserId = dto.FirstUserId;
            conversation.SecondUserId = dto.SecondUserId;
            conversation.CreatedAt = dto.CreatedAt;

            _context.SaveChanges();

            return _mapper.Map<ConversationReadDto>(conversation);
        }


    }
}
