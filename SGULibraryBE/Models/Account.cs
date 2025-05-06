using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models
{
    [Table("accounts")]
    public class Account : BaseModel
    {
        [Key]
        [Column("mssv")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StudentCode { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Faculty { get; set; }
        public string? Major { get; set; }
        [Column("otp_code")]
        public string? OTPCode { get; set; }
        [Column("expired_code")]
        public DateTime? OTPExpired { get; set; }

        [Column("avt")]
        public string? Avatar { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }
        public Role? Role { get; set; }
    }
}