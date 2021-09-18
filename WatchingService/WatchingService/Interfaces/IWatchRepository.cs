using System;
using System.Collections.Generic;
using WatchingService.DTOs.WatchDTOs;

namespace WatchingService.Interfaces
{
    public interface IWatchRepository
    {
        List<WatchReadDto> Get();
        WatchReadDto Get(Guid id);
        WatchConfirmationDto Create(WatchCreateDto dto);
        WatchConfirmationDto Update(Guid id, WatchCreateDto dto);
        void Delete(Guid id);
        List<WatchReadDto> GetWatchedForUser(int id);
        List<WatchReadDto> GetWhoWatchedUser(int id);
        bool CheckIfUserWatchOtherUser(int firsUser, int secondUser);
    }
}
