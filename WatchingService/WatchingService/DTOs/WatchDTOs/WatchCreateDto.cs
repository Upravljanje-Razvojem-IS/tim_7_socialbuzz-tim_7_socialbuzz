namespace WatchingService.DTOs.WatchDTOs
{
    public class WatchCreateDto
    {
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
