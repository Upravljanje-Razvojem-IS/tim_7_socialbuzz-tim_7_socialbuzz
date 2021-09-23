using LikeService.Data.Interfaces;
using LikeService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LikeService.Data.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly LikeContext _context;

        public LikeRepository(LikeContext context)
        {
            _context = context;
        }

        public IEnumerable<Like> Get()
        {
            return _context.Likes;
        }

        public Like Get(int id)
        {
            return _context.Likes
                .Where(like => like.Id == id)
                .FirstOrDefault();
        }

        public void Create(Like like)
        {
            _context.Likes.Add(like);
            _context.SaveChanges();
        }

        public void Update(Like like)
        {
            _context.Entry(like).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(Like like)
        {
            _context.Likes.Remove(like);
            _context.SaveChanges();
        }
    }
}
