using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.DTOs.UserDTOs
{
    public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserCreateDTO(string firstName, string lastName, string city, string address, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Address = address;
            Email = email;
            PasswordHash = passwordHash;
        }

        public UserCreateDTO()
        {
        }

    }

}
