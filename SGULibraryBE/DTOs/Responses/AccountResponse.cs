using SGULibraryBE.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.DTOs.Responses
{
    public class AccountResponse
    {
        public long StudentCode { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string? Faculty { get; set; }
        public string? Major { get; set; }
        public bool IsDeleted { get; set; }
        public Role? Role { get; set; }
    }
}
