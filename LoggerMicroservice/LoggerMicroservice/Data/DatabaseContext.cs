using LoggerMicroservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoggerMicroservice.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Log> Logs { get; set; }
    }
}
