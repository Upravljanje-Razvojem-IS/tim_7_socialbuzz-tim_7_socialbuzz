using RatingMicroservice.Entities;
using System.Collections.Generic;

namespace RatingMicroservice.Data
{
    public static class MockData
    {
        public static readonly List<User> Users = new List<User>
        {
            new User()
            {
                Id = 1,
                FirstName = "Nikolina",
                LastName = "Nikolic"
            },
            new User()
            {
                Id = 2,
                FirstName = "Jovana",
                LastName = "Jovanovic"
            }
        };

        public static readonly List<Item> Items = new List<Item>
        {
            new Item()
            {
                Id = 1,
                Name = "Item1",
                Price = 10,
                UserId = 1
            },

            new Item()
            {
                Id = 2,
                Name = "Item2",
                Price = 10,
                UserId = 2
            }
        };
    }
}
