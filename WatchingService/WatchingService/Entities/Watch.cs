using System;
using System.ComponentModel.DataAnnotations;

namespace WatchingService.Entities
{
    public class Watch
    {
        /// <summary>
        /// Id of Watch
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Watcher Id (Mocked user)
        /// </summary>
        [Required]
        public int WatcherId { get; set; }
        /// <summary>
        /// Watched Id (Mocked user)
        /// </summary>
        [Required]
        public int WatchedId { get; set; }
    }
}
