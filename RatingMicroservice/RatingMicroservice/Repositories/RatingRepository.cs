using AutoMapper;
using RatingMicroservice.CustomException;
using RatingMicroservice.Data;
using RatingMicroservice.DTOs;
using RatingMicroservice.Entities;
using RatingMicroservice.Interfaces;
using RatingMicroservice.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RatingMicroservice.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public RatingRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public RatingConfirmationDto Create(RatingCreateDto dto)
        {
            Item item = MockData.Items.FirstOrDefault(e => e.Id == dto.ItemId);
            User user = MockData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (item == null || user == null) 
                throw new BusinessException("Does not exist");

            Rating newRating = new Rating()
            {
                Id = Guid.NewGuid(),
                DateOfTrade = dto.DateOfTrade,
                RatingDescription = dto.RatingDescription,
                ItemRating = dto.ItemRating,
                SellerRating = dto.SellerRating,
                UserId = dto.UserId,
                ItemId = dto.ItemId
            };

            _context.Ratings.Add(newRating);

            _context.SaveChanges();

            _logger.Log("New Rating created!");

            return _mapper.Map<RatingConfirmationDto>(newRating);
        }

        public List<RatingReadDto> Get()
        {
            var list = _context.Ratings.ToList();

            _logger.Log("Get all Ratings!");

            return _mapper.Map<List<RatingReadDto>>(list);
        }

        public RatingReadDto Get(Guid id)
        {
            var entity = _context.Ratings.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get Rating by id!");

            return _mapper.Map<RatingReadDto>(entity);
        }

        public RatingConfirmationDto Update(Guid id, RatingCreateDto dto)
        {
            var ratingToUpdate = _context.Ratings.FirstOrDefault(e => e.Id == id);

            if (ratingToUpdate == null)
                throw new BusinessException("Rating does not exist");

            Item item = MockData.Items.FirstOrDefault(e => e.Id == dto.ItemId);
            User user = MockData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (item == null || user == null)
                throw new BusinessException("Does not exist");

            ratingToUpdate.DateOfTrade = dto.DateOfTrade;
            ratingToUpdate.RatingDescription = dto.RatingDescription;
            ratingToUpdate.ItemRating = dto.ItemRating;
            ratingToUpdate.SellerRating = dto.SellerRating;
            ratingToUpdate.UserId = dto.UserId;
            ratingToUpdate.ItemId = dto.ItemId;

            _context.SaveChanges();

            _logger.Log("Rating updated!");

            return _mapper.Map<RatingConfirmationDto>(ratingToUpdate);
        }

        public void Delete(Guid id)
        {
            var rating = _context.Ratings.FirstOrDefault(e => e.Id == id);

            if (rating == null)
                throw new BusinessException("Rating does not exist");

            _context.Ratings.Remove(rating);

            _logger.Log("Rating deleted!");

            _context.SaveChanges();
        }

        public List<RatingReadDto> GetAllRatingForItem(int itemId)
        {
            Item item = MockData.Items.FirstOrDefault(e => e.Id == itemId);

            if (item == null)
                throw new BusinessException("Item does not exist");

            var listOfRatings = _context.Ratings.Where(e => e.ItemId == itemId);

            return _mapper.Map<List<RatingReadDto>>(listOfRatings);
        }

        public List<RatingReadDto> GetAllRatingsOfTheUser(int userId)
        {
            User user = MockData.Users.FirstOrDefault(e => e.Id == userId);

            if (user == null)
                throw new BusinessException("User does not exist");

            var listOfRatings = _context.Ratings.Where(e => e.UserId == userId);

            return _mapper.Map<List<RatingReadDto>>(listOfRatings);
        }

        public List<RatingReadDto> GetRatingsOfItemForSpecificDate(int itemId, DateTime date)
        {
            Item item = MockData.Items.FirstOrDefault(e => e.Id == itemId);

            if (item == null)
                throw new BusinessException("Item does not exist");

            var listOfRatings = _context.Ratings.Where(e => e.ItemId == itemId && e.DateOfTrade == date);

            return _mapper.Map<List<RatingReadDto>>(listOfRatings);
        }
    }
}
