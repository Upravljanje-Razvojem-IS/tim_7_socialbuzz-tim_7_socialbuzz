using BlockingMicroservice.Entities;
using System.Collections.Generic;

namespace BlockingMicroservice.Data
{
    public static class MockedUsers
    {
        public static readonly List<User> Users = new List<User>
        {
            new User()
            {
                Id = 1,
                FirstName = "Tina",
                LastName = "Smith"
            },
            new User()
            {
                Id = 2,
                FirstName = "John",
                LastName = "Johnson"
            }
        };
    }
}
