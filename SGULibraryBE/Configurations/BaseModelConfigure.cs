using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models.Commons;

namespace SGULibraryBE.Configurations
{
    public class BaseModelConfigure<TModel> : IEntityTypeConfiguration<TModel> where TModel : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            /// ._.
        }
    }
}
