using SGULibraryBE.Models;

namespace SGULibraryBE.Repositories
{
    public interface IReservationRepository : IRepository<long, Reservation>
    {
        public Task<List<Reservation>> FindByDeviceId(long deviceId);
        public Task<List<Reservation>> FindByAccountId(long accountId);
    }
}
