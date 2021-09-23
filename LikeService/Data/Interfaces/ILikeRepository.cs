using LikeService.Models;
using System.Collections.Generic;

namespace LikeService.Data.Interfaces
{
    public interface ILikeRepository
    {
        IEnumerable<Like> Get();
        Like Get(int id);
        void Create(Like like);
        void Update(Like like);
        void Delete(Like like);
    }
}
