using SGULibraryBE.Models;

namespace SGULibraryBE.DTOs.Requests
{
    public class AccountViolationRequest
    {
        public DateTime DateCreate { get; set; }
        public long UserId { get; set; }
        public long ViolationId { get; set; }
    }
}
