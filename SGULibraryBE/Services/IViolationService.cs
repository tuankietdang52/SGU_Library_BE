using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services
{
    public interface IViolationService
    {
        public Task<Result<List<ViolationResponse>>> GetAll();
        public Task<Result<ViolationResponse>> FindById(long id);
    }
}