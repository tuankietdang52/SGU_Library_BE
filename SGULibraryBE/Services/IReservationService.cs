using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services
{
    public interface IReservationService
    {
        public Task<Result<List<ReservationResponse>>> GetAll();
        public Task<Result<ReservationResponse>> FindById(long id);
        public Task<Result<List<ReservationResponse>>> FindByAccountId(long accountId);
        public Task<Result<List<ReservationResponse>>> FindByDeviceId(long deviceId);
        public Task<Result<ReservationResponse>> Add(ReservationRequest request);
        public Task<Result> Update(long id, ReservationRequest request);
        public Task<Result> Delete(long id);
    }
}
