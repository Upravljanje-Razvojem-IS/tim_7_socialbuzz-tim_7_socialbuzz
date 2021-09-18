using System;
using System.ComponentModel.DataAnnotations;

namespace BlockingMicroservice.Entities
{
    public class Block
    {
        /// <summary>
        /// Id of Block
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Blocker Id (Mocked user)
        /// </summary>
        [Required]
        public int BlockerId { get; set; }
        /// <summary>
        /// Blocked Id (Mocked user)
        /// </summary>
        [Required]
        public int BlockedId { get; set; }
    }
}
