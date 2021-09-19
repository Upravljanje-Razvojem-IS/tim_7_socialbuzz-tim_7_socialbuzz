using System;
using System.ComponentModel.DataAnnotations;

namespace RatingMicroservice.Entities
{
    public class Rating
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Date of trade
        /// </summary>
        public DateTime DateOfTrade { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string RatingDescription { get; set; }
        /// <summary>
        /// Rating for item
        /// </summary>
        public int ItemRating { get; set; }
        /// <summary>
        /// Rating for seller
        /// </summary>
        public int SellerRating { get; set; }
        /// <summary>
        /// Rater User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Rated item id
        /// </summary>
        public int ItemId { get; set; }
    }
}
