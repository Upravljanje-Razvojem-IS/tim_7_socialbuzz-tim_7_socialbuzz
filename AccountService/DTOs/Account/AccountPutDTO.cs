﻿using System;

namespace AccountService.DTOs.Account
{
    public class AccountPutDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
