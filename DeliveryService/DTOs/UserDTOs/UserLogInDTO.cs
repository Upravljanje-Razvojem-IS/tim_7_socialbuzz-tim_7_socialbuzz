using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.DTOs.UserDTOs
{
    public class UserLogInDTO
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserLogInDTO(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
