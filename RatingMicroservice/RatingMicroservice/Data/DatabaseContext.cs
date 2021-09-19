using Microsoft.EntityFrameworkCore;
using RatingMicroservice.Entities;

namespace RatingMicroservice.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Rating> Ratings { get; set; }
    }
}
