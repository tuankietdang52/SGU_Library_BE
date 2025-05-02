using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGULibraryBE.Models
{
    [Table("reservations")]
    public class Reservation : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Quantity { get; set; }

        [Column("create_at")]
        public DateTime DateCreate { get; set; }

        [Column("date_borrow")]
        public DateTime DateBorrow { get; set; }

        [Column("date_return")]
        public DateTime DateReturn { get; set; }

        [Column("is_checked_out")]
        public bool IsCheckedOut { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
        public Account? User { get; set; }

        [Column("device_id")]
        public long DeviceId { get; set; }
        public Device? Device { get; set; }
    }
}
