using BlockingMicroservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlockingMicroservice.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
