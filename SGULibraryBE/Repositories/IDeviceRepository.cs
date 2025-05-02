using SGULibraryBE.Models;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories
{
    public interface IDeviceRepository : IRepository<long, Device>
    {
        public Task<List<Pair<Device, int>>> GetDevicesWithBorrowQuantity();
        public Task<int> GetDeviceBorrowQuantity(long id);
    }
}
