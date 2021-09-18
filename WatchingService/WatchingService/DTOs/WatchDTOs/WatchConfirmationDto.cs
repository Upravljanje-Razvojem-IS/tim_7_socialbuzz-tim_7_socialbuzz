using System;

namespace WatchingService.DTOs.WatchDTOs
{
    public class WatchConfirmationDto
    {
        /// <summary>
        /// Id of Watch
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Watcher Id (Mocked user)
        /// </summary>
        public int WatcherId { get; set; }
        /// <summary>
        /// Watched Id (Mocked user)
        /// </summary>
        public int WatchedId { get; set; }
    }
}
