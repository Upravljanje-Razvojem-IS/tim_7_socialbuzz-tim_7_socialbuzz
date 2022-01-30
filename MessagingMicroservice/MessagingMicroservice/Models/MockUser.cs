namespace MessagingMicroservice.Models
{
    public class MockUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public static class UserData
    {
        public static List<MockUser> Users = new List<MockUser>()
        {
            new MockUser()
            {
                Id = 1,
                Name = "Srdjan",
                LastName = "Miletix",
                Username = "srdjan",
                Email = "srdj@gmail.com"
            },
            new MockUser()
            {
                Id = 1,
                Name = "Mile",
                LastName = "Srdjic",
                Username = "mile",
                Email = "mil@gmail.com"
            }
        };
    }
}
