using RatingMicroservice.DTOs;
using System;
using System.Collections.Generic;

namespace RatingMicroservice.Interfaces
{
    public interface IRatingRepository
    {
        List<RatingReadDto> Get();
        RatingReadDto Get(Guid id);
        RatingConfirmationDto Create(RatingCreateDto dto);
        RatingConfirmationDto Update(Guid id, RatingCreateDto dto);
        void Delete(Guid id);
        List<RatingReadDto> GetAllRatingForItem(int itemId);
        List<RatingReadDto> GetAllRatingsOfTheUser(int userId);
        List<RatingReadDto> GetRatingsOfItemForSpecificDate(int itemId, DateTime date);

    }
}
