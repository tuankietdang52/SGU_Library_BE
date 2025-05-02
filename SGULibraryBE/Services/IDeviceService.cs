using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services
{
    public interface IDeviceService
    {
        public Task<Result<List<DeviceResponse>>> GetAll();
        public Task<Result<int>> GetDeviceBorrowQuantity(long id);
        public Task<Result<DeviceResponse>> FindById(long id);
        public Task<Result<DeviceResponse>> Add(DeviceRequest request);
        public Task<Result> Update(long id, DeviceRequest request);
        public Task<Result> Delete(long id);
    }
}
