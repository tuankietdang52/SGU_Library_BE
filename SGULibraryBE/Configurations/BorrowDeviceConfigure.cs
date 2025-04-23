using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public class BorrowDeviceConfigure : BaseModelConfigure<BorrowDevice>
    {
        public override void Configure(EntityTypeBuilder<BorrowDevice> builder)
        {
            base.Configure(builder);

            builder.HasKey(item => item.Id);

            builder.HasOne(item => item.User)
                   .WithMany()
                   .HasForeignKey(item => item.UserId);

            builder.HasOne(item => item.Device)
                   .WithMany()
                   .HasForeignKey(item => item.DeviceId);
        }
    }
}
