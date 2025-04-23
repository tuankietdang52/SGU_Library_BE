using SGULibraryBE.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models
{
    [Table("devices")]
    public class Device : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }

        [Column("img")]
        public string? Image { get; set; }
        public string Description { get; set; } = string.Empty;

        [Column("is_available")]
        public bool IsAvailable { get; set; }
    }
}
