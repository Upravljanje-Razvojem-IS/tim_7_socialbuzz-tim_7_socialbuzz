namespace BlockingMicroservice.DTOs.BlockDTOs
{
    public class BlockCreateDto
    {
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
