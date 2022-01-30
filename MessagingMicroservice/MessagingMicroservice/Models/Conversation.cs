﻿namespace MessagingMicroservice.Models
{
    public class Conversation
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First user id
        /// </summary>
        public int FirstUserId { get; set; }
        /// <summary>
        /// Second user id
        /// </summary>
        public int SecondUserId { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }
        public List<Message> Messages { get; set; }
    }
}
