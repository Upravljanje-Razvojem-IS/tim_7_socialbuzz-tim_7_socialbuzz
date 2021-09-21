using AutoMapper;
using LoggerMicroservice.CustomExceptionn;
using LoggerMicroservice.Data;
using LoggerMicroservice.DTOs;
using LoggerMicroservice.Entities;
using LoggerMicroservice.Interfaces;
using LoggerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggerMicroservice.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public LogRepository(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public LogConfirmationDto Create(LogCreateDto dto)
        {
            Log newLog = new Log()
            {
                Id = Guid.NewGuid(),
                Text = dto.Text,
                CreatedAt = dto.CreatedAt
            };

            _context.Logs.Add(newLog);

            _context.SaveChanges();

            return _mapper.Map<LogConfirmationDto>(newLog);
        }

        public List<LogReadDto> Get()
        {
            var list = _context.Logs.ToList();

            return _mapper.Map<List<LogReadDto>>(list);
        }

        public LogReadDto Get(Guid id)
        {
            var entity = _context.Logs.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<LogReadDto>(entity);
        }

        public LogConfirmationDto Update(Guid id, LogCreateDto dto)
        {
            var log = _context.Logs.FirstOrDefault(e => e.Id == id);

            if (log == null)
                throw new BusinessException("Log does not exist");

            log.Text = dto.Text;
            log.CreatedAt = dto.CreatedAt;

            _context.SaveChanges();

            return _mapper.Map<LogConfirmationDto>(log);
        }

        public void Delete(Guid id)
        {
            var log = _context.Logs.FirstOrDefault(e => e.Id == id);

            if (log == null)
                throw new BusinessException("Log does not exist");

            _context.Logs.Remove(log);

            _context.SaveChanges();
        }

        public List<LogReadDto> GetAllLogsForDate(DateTime date)
        {
            var list = _context.Logs.Where(e => e.CreatedAt == date);

            return _mapper.Map<List<LogReadDto>>(list);
        }

        public List<LogReadDto> GetAllLogsBetweenTwoDates(BetweenDates dates)
        {
            var list = _context.Logs.Where(e => e.CreatedAt >= dates.StartDate && e.CreatedAt <= dates.EndDate);

            return _mapper.Map<List<LogReadDto>>(list);
        }

        public List<LogReadDto> SearchLogs(string text)
        {
            var list = _context.Logs.Where(e => e.Text.Contains(text));

            return _mapper.Map<List<LogReadDto>>(list);
        }
    }
}
