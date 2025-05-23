﻿using Mapster;
using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models
{
    [Table("account_violation")]
    public class AccountViolation : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("create_at")]
        public DateTime DateCreate { get; set; }

        [Column("mssv")]
        public long UserId { get; set; }
        public Account? User { get; set; }

        [AdaptIgnore]
        public string? Status { get; set; }

        [NotMapped]
        public AccountViolationStatus EStatus
        {
            get => Enum.Parse<AccountViolationStatus>(Status!);
            set
            {
                Status = value.ToString();
            }
        }

        [Column("ban_expired")]
        public DateTime BanExpired { get; set; }
        public long Compensation { get; set; }

        [Column("violation_id")]
        public long ViolationId { get; set; }
        public Violation? Violation { get; set; }
    }

    public enum AccountViolationStatus
    {
        Handled,
        BeingProcessed
    }
}
