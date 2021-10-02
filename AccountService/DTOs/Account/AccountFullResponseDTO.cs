using AccountService.DTOs.AccRole;
using System;

namespace AccountService.DTOs.Account
{
    public class AccountFullResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
        public AccRoleResponseDTO Role { get; set; }
    }
}
