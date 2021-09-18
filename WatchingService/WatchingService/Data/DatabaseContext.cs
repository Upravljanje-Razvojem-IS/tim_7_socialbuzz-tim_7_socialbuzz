using Microsoft.EntityFrameworkCore;
using WatchingService.Entities;

namespace WatchingService.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Watch> Watches { get; set; }
    }
}
