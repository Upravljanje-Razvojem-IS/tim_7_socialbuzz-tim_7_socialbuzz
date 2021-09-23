using LikeService.Models;
using Microsoft.EntityFrameworkCore;

namespace LikeService.Data
{
    public class LikeContext : DbContext
    {
        public DbSet<Like> Likes { get; set; }

        public LikeContext(DbContextOptions<LikeContext> options) : base(options) { }
    }
}
