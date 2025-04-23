using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public class DeviceConfigure : BaseModelConfigure<Device>
    {
        public override void Configure(EntityTypeBuilder<Device> builder)
        {
            base.Configure(builder);
            builder.HasKey(u => u.Id);
        }
    }
}
