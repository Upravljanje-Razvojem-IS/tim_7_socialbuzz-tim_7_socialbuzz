namespace MessagingMicroservice.DTOs
{
    /// <summary>
    /// Create dto message
    /// </summary>
    public class MessageCreateDto
    {
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// sender id
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// Conversation Id
        /// </summary>
        public int ConversationId { get; set; }
        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// updated at
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Is read
        /// </summary>
        public bool IsRead { get; set; }
    }
}
