using LikeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeService.Data.Interfaces
{
    public interface IAccountRepository
    {
        Account Get(Guid accoutnUid);
    }
}
