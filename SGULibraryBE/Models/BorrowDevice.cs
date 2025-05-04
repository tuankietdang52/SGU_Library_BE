using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models
{
    [Table("borrow_devices")]
    public class BorrowDevice : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Quantity { get; set; }

        [Column("create_at")]
        public DateTime DateCreate { get; set; }

        [Column("date_borrow")]
        public DateTime DateBorrow { get; set; }

        [Column("date_return_expected")]
        public DateTime DateReturnExpected { get; set; }

        [Column("date_return")]
        public DateTime? DateReturn { get; set; }

        [Column("is_return")]
        public bool IsReturn { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
        public Account? User { get; set; }

        [Column("device_id")]
        public long DeviceId { get; set; }
        public Device? Device { get; set; }
    }
}