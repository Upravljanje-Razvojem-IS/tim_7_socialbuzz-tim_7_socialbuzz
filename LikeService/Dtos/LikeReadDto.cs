using System;

namespace LikeService.Dtos
{
    public class LikeReadDto
    {
        /// <summary>
        /// Like id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Uid of the account that liked.
        /// </summary>
        public Guid AccountUid { get; set; }

        /// <summary>
        /// Ad id of the ad that was liked.
        /// </summary>
        public int AdId { get; set; }

        /// <summary>
        /// Type of like.
        /// </summary>
        public string Type { get; set; }
    }
}
