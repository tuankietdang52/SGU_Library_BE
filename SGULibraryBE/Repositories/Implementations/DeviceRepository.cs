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
                                        var count = appDbContext.BorrowDevices.Where(e => e.DeviceId == item.Id && !e.IsDeleted && !e.IsReturn)
                                                                              .Sum(e => e.Quantity);
                                        count += appDbContext.Reservations.Where(e => e.DeviceId == item.Id && !e.IsDeleted && !e.IsCheckedOut)
                                                                          .Sum(e => e.Quantity);
                                        return new Pair<Device, int>(item, count);
                                    })];
        }

        public async Task<int> GetDeviceBorrowQuantity(long id)
        {
            if (_dbContext is not AppDbContext appDbContext)
                throw new InvalidOperationException("dbContext must be AppDbContext");

            var device = await _dbSet.FirstOrDefaultAsync(item => item.Id == id);
            if (device is null) return -1;

            var count = appDbContext.BorrowDevices.Where(e => e.DeviceId == device.Id && !e.IsDeleted && !e.IsReturn)
                                                  .Sum(e => e.Quantity);
            count += appDbContext.Reservations.Where(e => e.DeviceId == device.Id && !e.IsDeleted && !e.IsCheckedOut)
                                              .Sum(e => e.Quantity);

            return count;
        }
    }
}
