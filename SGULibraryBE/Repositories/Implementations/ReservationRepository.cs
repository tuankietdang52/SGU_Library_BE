using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public class ReservationRepository : Repository<long, Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext)
        {
            References.AddMultiple([
                r => r.User!,
                r => r.Device!
            ]);
        }

        public override async Task<Reservation?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<List<Reservation>> FindByAccountId(long accountId)
        {
            return await _dbSet.IncludeAll(References)
                              .Where(item => item.UserId == accountId)
                              .ToListAsync();
        }

        public async Task<List<Reservation>> FindByDeviceId(long deviceId)
        {
            return await _dbSet.IncludeAll(References)
                               .Where(item => item.DeviceId == deviceId)
                               .ToListAsync();
        }
    }
}
