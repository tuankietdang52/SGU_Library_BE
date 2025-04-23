using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGULibraryBE.Models.Commons
{
    public abstract class BaseModel
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}