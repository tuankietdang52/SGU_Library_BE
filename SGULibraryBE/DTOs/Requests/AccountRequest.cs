using SGULibraryBE.Models;

namespace SGULibraryBE.DTOs.Requests
{
    public class AccountRequest
    {
        public long? StudentCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Faculty { get; set; }
        public string? Major { get; set; }
        public long RoleId { get; set; }
    }
}