using System;

namespace AccountService.DTOs.Account
{
    public class AccountBasicResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
