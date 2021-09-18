using AutoMapper;
using BlockingMicroservice.CustomException;
using BlockingMicroservice.Data;
using BlockingMicroservice.DTOs.BlockDTOs;
using BlockingMicroservice.Entities;
using BlockingMicroservice.Interfaces;
using BlockingMicroservice.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockingMicroservice.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public BlockRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public BlockConfirmationDto Create(BlockCreateDto dto)
        {
            if (dto.BlockerId == dto.BlockedId)
                throw new BusinessException("Can not block yourself");

            var blocked = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.BlockedId);
            var blocker = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.BlockerId);

            if (blocked == null || blocker == null)
                throw new BusinessException("User does not exist");

            Block newBlock = new Block()
            {
                Id = Guid.NewGuid(),
                BlockedId = dto.BlockedId,
                BlockerId = dto.BlockerId
            };

            _context.Blocks.Add(newBlock);

            _context.SaveChanges();

            _logger.Log("New block created!");

            return _mapper.Map<BlockConfirmationDto>(newBlock);
        }

        public List<BlockReadDto> Get()
        {
            var list = _context.Blocks.ToList();

            _logger.Log("Get all blocks!");

            return _mapper.Map<List<BlockReadDto>>(list);
        }

        public BlockReadDto Get(Guid id)
        {
            var entity = _context.Blocks.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get block by id!");

            return _mapper.Map<BlockReadDto>(entity);
        }

        public BlockConfirmationDto Update(Guid id, BlockCreateDto dto)
        {
            if (dto.BlockerId == dto.BlockedId)
                throw new BusinessException("Can not block yourself");

            var blocked = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.BlockedId);
            var blocker = MockedUsers.Users.FirstOrDefault(e => e.Id == dto.BlockerId);

            if (blocked == null || blocker == null)
                throw new BusinessException("User does not exist");

            var block = _context.Blocks.FirstOrDefault(e => e.Id == id);

            if (block == null)
                throw new BusinessException("Block does not exist");

            block.BlockerId = dto.BlockerId;
            block.BlockedId = dto.BlockedId;

            _context.SaveChanges();

            _logger.Log("Block updated!");

            return _mapper.Map<BlockConfirmationDto>(block);
        }

        public void Delete(Guid id)
        {
            var Block = _context.Blocks.FirstOrDefault(e => e.Id == id);

            if (Block == null)
                throw new BusinessException("Block does not exist");

            _context.Blocks.Remove(Block);

            _logger.Log("Block deleted!");

            _context.SaveChanges();
        }

        public List<BlockReadDto> GetBlockedForUser(int id)
        {
            var user = MockedUsers.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");

            var list = _context.Blocks.Where(e => e.BlockerId == id);

            return _mapper.Map<List<BlockReadDto>>(list);
        }

        public List<BlockReadDto> GetWhoBlockedUser(int id)
        {
            var user = MockedUsers.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");

            var list = _context.Blocks.Where(e => e.BlockedId == id);

            return _mapper.Map<List<BlockReadDto>>(list);
        }
    }
}
