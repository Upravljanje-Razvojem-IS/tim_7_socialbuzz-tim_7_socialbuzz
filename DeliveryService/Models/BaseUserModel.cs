using DeliveryService.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    /// <summary>
    /// Entity class which represents user
    /// </summary>
    public class BaseUserModel
    {

        /// <summary>
        /// an identifier for the user
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///  user's firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// user's  lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// adresa
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// email of user
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// hashed password
        /// </summary>
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
        public static BaseUserModel FromUser(UserCreateDTO user)
        {
            var userdb = new BaseUserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Address = user.Address,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };
            return userdb;
        }
    }
}
