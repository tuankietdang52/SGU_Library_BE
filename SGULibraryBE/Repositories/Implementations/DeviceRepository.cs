using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public class DeviceRepository : Repository<long, Device>, IDeviceRepository
    {
        public DeviceRepository(AppDbContext dbContext) : base(dbContext)
        {
           
        }

        public override async Task<Device?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Pair<Device, int>>> GetDevicesWithBorrowQuantity()
        {
            if (_dbContext is not AppDbContext appDbContext)
                throw new InvalidOperationException("dbContext must be AppDbContext");

            return [.. (await _dbSet.ToListAsync())
                                    .Select(item =>
                                    {
                                        var count = appDbContext.BorrowDevices.Count(e => e.DeviceId == item.Id);
                                        return new Pair<Device, int>(item, count);
                                    })];
        }
    }
}
