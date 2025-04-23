using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public class BorrowDeviceRepository : Repository<long, BorrowDevice>, IBorrowDeviceRepository
    {
        public BorrowDeviceRepository(AppDbContext dbContext) : base(dbContext)
        {
            References.AddMultiple([
                item => item.User!,
                item => item.Device!
            ]);
        }

        public override async Task<BorrowDevice?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}
