using System;

namespace LikeService.Models
{
    public class Like
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
        /// Like type.
        /// </summary>
        public string Type { get; set; }
    }
}
