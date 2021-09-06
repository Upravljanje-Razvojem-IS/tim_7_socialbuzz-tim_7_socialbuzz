﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Logger
{
    public class LoggerMockRepository : ILoggerMockRepository
    {
        public void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception)
        {
            Task.Run(() =>
            {
                Thread.Sleep(500);
            });
        }
    }
}
