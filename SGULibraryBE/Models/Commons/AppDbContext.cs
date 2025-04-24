using Microsoft.EntityFrameworkCore;

namespace SGULibraryBE.Models.Commons
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<BorrowDevice> BorrowDevices { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<AccountViolation> AccountViolations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}