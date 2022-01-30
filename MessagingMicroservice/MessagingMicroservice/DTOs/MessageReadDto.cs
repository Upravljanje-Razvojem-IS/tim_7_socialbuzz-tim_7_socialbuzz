namespace MessagingMicroservice.DTOs
{
    /// <summary>
    /// Message read dto
    /// </summary>
    public class MessageReadDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
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
