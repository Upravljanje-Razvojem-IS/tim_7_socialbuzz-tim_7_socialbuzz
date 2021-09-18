using BlockingMicroservice.DTOs.BlockDTOs;
using System;
using System.Collections.Generic;

namespace BlockingMicroservice.Interfaces
{
    public interface IBlockRepository
    {
        List<BlockReadDto> Get();
        BlockReadDto Get(Guid id);
        BlockConfirmationDto Create(BlockCreateDto dto);
        BlockConfirmationDto Update(Guid id, BlockCreateDto dto);
        void Delete(Guid id);
        List<BlockReadDto> GetBlockedForUser(int id);
        List<BlockReadDto> GetWhoBlockedUser(int id);
    }
}
