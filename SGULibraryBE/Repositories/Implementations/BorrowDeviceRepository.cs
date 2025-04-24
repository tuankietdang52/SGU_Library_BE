using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;
using System.Threading.Tasks;

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

        public async Task<List<BorrowDevice>> FindByAccountId(long accountId)
        {
            return await _dbSet.IncludeAll(References)
                                .Where(item => item.UserId == accountId)
                                .ToListAsync();
        }

        public async Task<List<BorrowDevice>> FindByDeviceId(long deviceId)
        {
            return await _dbSet.IncludeAll(References)
                               .Where(item => item.DeviceId == deviceId)
                               .ToListAsync();
        }

        public override async Task<BorrowDevice?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}
