using Microsoft.AspNetCore.Identity;
using System;

namespace AccountService.Entities
{
    public class Account : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public Guid RoleId { get; set; }
        public AccRole Role { get; set; }
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        
        public Account() : base()
        {
            
        }
    }
}
