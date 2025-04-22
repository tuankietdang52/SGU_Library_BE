﻿using SGULibraryBE.Models;

namespace SGULibraryBE.DTOs.Requests
{
    public class AccountRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public long RoleId { get; set; }
    }
}