using System;
using System.ComponentModel.DataAnnotations;

namespace LoggerMicroservice.Entities
{
    public class Log
    {
        /// <summary>
        /// Id of log
        /// </summary>
        [Key]
        public Guid Id { get; set; }
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
