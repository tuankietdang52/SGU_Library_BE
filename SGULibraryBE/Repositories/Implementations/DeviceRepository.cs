using Microsoft.EntityFrameworkCore;
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
    }
}
