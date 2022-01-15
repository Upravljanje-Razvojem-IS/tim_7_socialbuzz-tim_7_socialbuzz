using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
