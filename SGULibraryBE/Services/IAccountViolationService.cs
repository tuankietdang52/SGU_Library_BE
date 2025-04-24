using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services
{
    public interface IAccountViolationService
    {
        public Task<Result<AccountViolationResponse>> FindById(long id);
        public Task<Result<List<AccountViolationResponse>>> GetAll();
        public Task<Result<List<AccountViolationResponse>>> FindByAccountId(long id);
        public Task<Result<List<AccountViolationResponse>>> FindByViolationId(long id);
        public Task<Result<AccountViolationResponse>> IsAccountViolate(long accountId);
    }
}
