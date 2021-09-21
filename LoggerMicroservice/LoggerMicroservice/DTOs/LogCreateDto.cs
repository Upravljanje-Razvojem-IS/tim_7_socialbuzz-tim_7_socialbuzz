using System;

namespace LoggerMicroservice.DTOs
{
    public class LogCreateDto
    {
        /// <summary>
        /// Log text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Time of log creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
