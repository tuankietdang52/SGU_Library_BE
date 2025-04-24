using Mapster;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Repositories;
using SGULibraryBE.Repositories.Implementations;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class AccountViolationService : IAccountViolationService
    {
        private readonly IUnitOfWork unitOfWork;
        private IAccountViolationRepository AccountViolationRepository => unitOfWork.AccountViolationRepository;

        private readonly IAccountService _accountService;

        public AccountViolationService(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            this.unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Result<List<AccountViolationResponse>>> FindByAccountId(long id)
        {
            var response = await AccountViolationRepository.FindByAccountIdAsync(id);
            return Result<List<AccountViolationResponse>>.Success(response.Adapt<List<AccountViolationResponse>>());
        }

        public async Task<Result<AccountViolationResponse>> FindById(long id)
        {
            var response = await AccountViolationRepository.FindByIdAsync(id);

            if (response is null)
            {
                return Result<AccountViolationResponse>.Failure(Error.NotFound($"Account Violation with id {id} does not exist"));
            }

            return Result<AccountViolationResponse>.Success(response.Adapt<AccountViolationResponse>());
        }

        public async Task<Result<List<AccountViolationResponse>>> FindByViolationId(long id)
        {
            var response = await AccountViolationRepository.FindByViolationIdAsync(id);
            return Result<List<AccountViolationResponse>>.Success(response.Adapt<List<AccountViolationResponse>>());
        }

        public async Task<Result<List<AccountViolationResponse>>> GetAll()
        {
            var response = await AccountViolationRepository.GetAllAsync();
            return Result<List<AccountViolationResponse>>.Success(response.Adapt<List<AccountViolationResponse>>());
        }

        public async Task<Result<AccountViolationResponse>> IsAccountViolate(long accountId)
        {
            var result =  await _accountService.FindById(accountId);
            if (!result.IsSuccess)
            {
                return Result<AccountViolationResponse>.Failure(result.Error);
            }

            var response = await AccountViolationRepository.IsAccountViolate(accountId);
            return Result<AccountViolationResponse>.Success(response?.Adapt<AccountViolationResponse>());
        }
    }
}
