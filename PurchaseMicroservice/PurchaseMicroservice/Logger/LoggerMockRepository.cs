﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerMockRepository : ILoggerMockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="requestId"></param>
        /// <param name="previousRequestId"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception)
        {
            Task.Run(() =>
            {
                Thread.Sleep(500);
            });
        }
    }
}
