using LoggerMicroservice.DTOs;
using LoggerMicroservice.Models;
using System;
using System.Collections.Generic;

namespace LoggerMicroservice.Interfaces
{
    public interface ILogRepository
    {
        List<LogReadDto> Get();
        LogReadDto Get(Guid id);
        LogConfirmationDto Create(LogCreateDto dto);
        LogConfirmationDto Update(Guid id, LogCreateDto dto);
        void Delete(Guid id);
        List<LogReadDto> GetAllLogsForDate(DateTime date);
        List<LogReadDto> GetAllLogsBetweenTwoDates(BetweenDates dates);
        List<LogReadDto> SearchLogs(string text);

    }
}
