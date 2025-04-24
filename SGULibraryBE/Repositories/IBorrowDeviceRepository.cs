using SGULibraryBE.Models;

namespace SGULibraryBE.Repositories
{
    public interface IBorrowDeviceRepository : IRepository<long, BorrowDevice>
    {
        public Task<List<BorrowDevice>> FindByDeviceId(long deviceId);
        public Task<List<BorrowDevice>> FindByAccountId(long accountId);
    }
}