using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public class AccountConfigure : BaseModelConfigure<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Role)
                   .WithMany()
                   .HasForeignKey(u => u.RoleId);
        }
    }
}
