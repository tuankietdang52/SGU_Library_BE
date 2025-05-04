using SGULibraryBE.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.DTOs.Requests
{
    public class AccountViolationRequest
    {
        public DateTime DateCreate { get; set; }
        public long UserId { get; set; }
        public long ViolationId { get; set; }
        public AccountViolationStatus Status { get; set; }
        public DateTime BanExpired { get; set; }
        public long Compensation { get; set; }
    }
}
