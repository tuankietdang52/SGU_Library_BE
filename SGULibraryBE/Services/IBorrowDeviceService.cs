using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services
{
    public interface IBorrowDeviceService
    {
        public Task<Result<List<BorrowDeviceResponse>>> GetAll();
        public Task<Result<BorrowDeviceResponse>> FindById(long id);
        public Task<Result<BorrowDeviceResponse>> Add(BorrowDeviceRequest request);
        public Task<Result> Update(long id, BorrowDeviceRequest request);
        public Task<Result> Delete(long id);
    }
}
