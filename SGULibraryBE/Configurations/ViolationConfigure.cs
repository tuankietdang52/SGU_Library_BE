using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public class ViolationConfigure : BaseModelConfigure<Violation>
    {
        public override void Configure(EntityTypeBuilder<Violation> builder)
        {
            base.Configure(builder);
            builder.HasKey(v => v.Id);
        }
    }
}
