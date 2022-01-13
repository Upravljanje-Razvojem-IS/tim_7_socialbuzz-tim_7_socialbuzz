using DeliveryService.Database;
using DeliveryService.Interface;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Repository
{
    public class BaseUserModelRepository : IBaseUserModelRepository
    {


        private readonly BaseUserModelRepository user;
        private readonly DatabaseContext Context;

        public BaseUserModelRepository(DatabaseContext context)
        {
            Context = context;
        }


        public BaseUserModel Add(BaseUserModel userModel)
        {
            Context.Add(userModel);
            Context.SaveChanges();
            return userModel;
        }

        public void Delete(Guid id)
        {
            var user = Context.Orders.Where(e => e.Id == id).FirstOrDefault();
            if (user == null)
                throw new ArgumentNullException("BaseUserModel");
            else
            {
                Context.Remove(user);
                Context.SaveChanges();
            }
        }

        public List<BaseUserModel> GetAll()
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

            return users;
        }

        public BaseUserModel GetById(Guid id)
        {
            BaseUserModel user = null;

            user = Context.Users.Where(e => e.Id == id).Select(c => new BaseUserModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                City = c.City,
                Address= c.Address,
                Email = c.Email,
                PasswordHash = c.PasswordHash
            }).FirstOrDefault();
            return user;
        }

        public BaseUserModel Update(Guid id, BaseUserModel userModel)
        {
            var updatedUserModel = Context.Users.FirstOrDefault(x => x.Id == userModel.Id);

            if (updatedUserModel == null)
                throw new ArgumentNullException("User");

            
            updatedUserModel.FirstName = userModel.FirstName;
            updatedUserModel.LastName = userModel.LastName;
            updatedUserModel.City = userModel.City;
            updatedUserModel.Address = userModel.Address;
            updatedUserModel.Email = userModel.Email;
            updatedUserModel.PasswordHash= userModel.PasswordHash;

            Context.SaveChanges();
            return updatedUserModel;
        }
    }
}
