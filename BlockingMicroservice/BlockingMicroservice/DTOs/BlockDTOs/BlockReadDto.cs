using System;

namespace BlockingMicroservice.DTOs.BlockDTOs
{
    public class BlockReadDto
    {
        /// <summary>
        /// Id of Block
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Blocker Id (Mocked user)
        /// </summary>
        public int BlockerId { get; set; }
        /// <summary>
        /// Blocked Id (Mocked user)
        /// </summary>
        public int BlockedId { get; set; }
    }
}
