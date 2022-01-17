using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.mockData
{
    public static class mockUser
    {
        public static List<BaseUserModel> Users = new List<BaseUserModel>
        {
            new BaseUserModel()
            {
                Id = new Guid("{5b85f839-ad92-455e-bcab-ac9ffe713e56}"),
                FirstName = "Mica",
                LastName = "Micunovic",
                City = "Novi Sad",
                Address = "Valentina Vodnika 8",
                Email = "mica@gmail.com",
                PasswordHash = "mica1850"
            },

            new BaseUserModel()
            {
                Id = new Guid("{5e98a3a2-3429-41f1-b722-566593becd3d}"),
                FirstName = "Masa",
                LastName = "Pantovic",
                City = "Beograd",
                Address = "Marsala Tita 11",
                Email = "masa@gmail.com",
                PasswordHash = "masamasna1"
            }
        };
    }
}
