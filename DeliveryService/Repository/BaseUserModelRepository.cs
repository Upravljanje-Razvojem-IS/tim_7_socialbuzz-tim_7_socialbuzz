using AutoMapper;
using DeliveryService.Database;
using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    /// <summary>
    /// User repository 
    /// </summary>
    public class BaseUserModelRepository : IBaseUserModelRepository
    {

        private readonly DatabaseContext Context;
        private readonly IMapper Mapper;

        public BaseUserModelRepository(DatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }


        /// <summary>
        /// create new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserDTO Add(UserCreateDTO userModel)
        {
            BaseUserModel user = new BaseUserModel()
            {
                Id = Guid.NewGuid(),
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                City = userModel.City,
                Address = userModel.Address,
                Email = userModel.Email,
                PasswordHash = userModel.PasswordHash
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            return Mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var user = Context.Users.Where(e => e.Id == id).FirstOrDefault();
            if (user == null)
                throw new ArgumentNullException("User does not exist");
            else
            {
                Context.Remove(user);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// retursn all users
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAll()
        {
            var users = Context.Users.Select(c => new BaseUserModel()
            {
                Id = c.Id,
                FirstName= c.FirstName,
                LastName = c.LastName,
                City = c.City,
                Address = c.Address,
                Email = c.Email,
                PasswordHash = c.PasswordHash
            }).ToList();

            return Mapper.Map<List<UserDTO>>(users);
        }

        /// <summary>
        /// return user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO GetById(Guid id)
        {

            var user = Context.Users.Where(e => e.Id == id).Select(c => new BaseUserModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                City = c.City,
                Address= c.Address,
                Email = c.Email,
                PasswordHash = c.PasswordHash
            }).FirstOrDefault();

            return Mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserConfirmDTO Update(Guid id, UserDTO userModel)
        {
            var updatedUserModel = Context.Users.FirstOrDefault(x => x.Id == id);

            if (updatedUserModel == null)
                throw new ArgumentNullException("User does not exist");

            
            updatedUserModel.FirstName = userModel.FirstName;
            updatedUserModel.LastName = userModel.LastName;
            updatedUserModel.City = userModel.City;
            updatedUserModel.Address = userModel.Address;
            updatedUserModel.Email = userModel.Email;
            updatedUserModel.PasswordHash= userModel.PasswordHash;

            Context.SaveChanges();
            return Mapper.Map<UserConfirmDTO>(updatedUserModel);
        }
    }
}
