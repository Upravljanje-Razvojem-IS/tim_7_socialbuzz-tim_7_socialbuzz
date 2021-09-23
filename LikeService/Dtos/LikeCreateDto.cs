using System;

namespace LikeService.Dtos
{
    public class LikeCreateDto
    {
        /// <summary>
        /// Uid of the account that liked.
        /// </summary>
        public Guid AccountUid { get; set; }

        /// <summary>
        /// Ad id of the ad that was liked.
        /// </summary>
        public int AdId { get; set; }

        /// <summary>
        /// Like type
        /// </summary>
        public string Type { get; set; }

    }
}
