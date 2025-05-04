using SGULibraryBE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGULibraryBE.DTOs.Responses
{
    public class AccountViolationResponse
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public AccountViolationStatus Status { get; set; }
        public DateTime BanExpired { get; set; }
        public long Compensation { get; set; }
        public AccountResponse? User { get; set; }
        public ViolationResponse? Violation { get; set; }
    }
}
