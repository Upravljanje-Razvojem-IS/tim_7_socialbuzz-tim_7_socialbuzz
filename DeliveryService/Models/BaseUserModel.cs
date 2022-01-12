using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class BaseUserModel
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public BaseUserModel(Guid id, string firstName, string lastName, 
            string city, string address,
            string email, string passwordHash)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Address = address;
            Email = email;
            PasswordHash = passwordHash;
        }

        public BaseUserModel()
        {

        }
    }
}
