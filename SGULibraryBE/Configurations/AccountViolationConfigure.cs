using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public class AccountViolationConfigure : BaseModelConfigure<AccountViolation>
    {
        public override void Configure(EntityTypeBuilder<AccountViolation> builder)
        {
            base.Configure(builder);

            builder.HasKey(item => item.Id);

            builder.HasOne(item => item.User)
                   .WithMany()
                   .HasForeignKey(item => item.UserId);

            builder.HasOne(item => item.Violation)
                   .WithMany()
                   .HasForeignKey(item => item.ViolationId);
        }
    }
}
