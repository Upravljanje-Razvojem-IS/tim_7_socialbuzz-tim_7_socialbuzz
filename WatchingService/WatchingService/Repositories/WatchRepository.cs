using AutoMapper;
using WatchingService.CustomException;
using WatchingService.Data;
using WatchingService.Interfaces;
using WatchingService.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using WatchingService.Entities;
using WatchingService.DTOs.WatchDTOs;

namespace WatchingService.Repositories
{
    public class WatchRepository : IWatchRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public WatchRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public WatchConfirmationDto Create(WatchCreateDto dto)
        {
            if (dto.WatcherId == dto.WatchedId)
                throw new BusinessException("Can not Watch yourself");

            var Watched = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.WatchedId);
            var Watcher = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.WatcherId);

            if (Watched == null || Watcher == null)
                throw new BusinessException("User does not exist");

            Watch newWatch = new Watch()
            {
                Id = Guid.NewGuid(),
                WatchedId = dto.WatchedId,
                WatcherId = dto.WatcherId
            };

            _context.Watches.Add(newWatch);

            _context.SaveChanges();

            _logger.Log("New Watch created!");

            return _mapper.Map<WatchConfirmationDto>(newWatch);
        }

        public List<WatchReadDto> Get()
        {
            var list = _context.Watches.ToList();

            _logger.Log("Get all Watchs!");

            return _mapper.Map<List<WatchReadDto>>(list);
        }

        public WatchReadDto Get(Guid id)
        {
            var entity = _context.Watches.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get Watch by id!");

            return _mapper.Map<WatchReadDto>(entity);
        }

        public WatchConfirmationDto Update(Guid id, WatchCreateDto dto)
        {
            if (dto.WatcherId == dto.WatchedId)
                throw new BusinessException("Can not Watch yourself");

            var Watched = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.WatchedId);
            var Watcher = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.WatcherId);

            if (Watched == null || Watcher == null)
                throw new BusinessException("User does not exist");

            var Watch = _context.Watches.FirstOrDefault(e => e.Id == id);

            if (Watch == null)
                throw new BusinessException("Watch does not exist");

            Watch.WatcherId = dto.WatcherId;
            Watch.WatchedId = dto.WatchedId;

            _context.SaveChanges();

            _logger.Log("Watch updated!");

            return _mapper.Map<WatchConfirmationDto>(Watch);
        }

        public void Delete(Guid id)
        {
            var Watch = _context.Watches.FirstOrDefault(e => e.Id == id);

            if (Watch == null)
                throw new BusinessException("Watch does not exist");

            _context.Watches.Remove(Watch);

            _logger.Log("Watch deleted!");

            _context.SaveChanges();
        }

        public List<WatchReadDto> GetWatchedForUser(int id)
        {
            var user = MockedUsers.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");

            var list = _context.Watches.Where(e => e.WatcherId == id);

            return _mapper.Map<List<WatchReadDto>>(list);
        }

        public List<WatchReadDto> GetWhoWatchedUser(int id)
        {
            var user = MockedUsers.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");

            var list = _context.Watches.Where(e => e.WatchedId == id);

            return _mapper.Map<List<WatchReadDto>>(list);
        }

        public bool CheckIfUserWatchOtherUser(int firsUser, int secondUser)
        {
            var watch = _context.Watches.FirstOrDefault(e => e.WatcherId == firsUser && e.WatchedId == secondUser);

            return watch == null ? false : true;
        }

    }
}
