using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models
{
    [Table("accounts")]
    public class Account : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        [Column("avt")]
        public string? Avatar { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }
        public Role? Role { get; set; }
    }
}