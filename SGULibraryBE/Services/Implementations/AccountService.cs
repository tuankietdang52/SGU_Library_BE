using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;

namespace SGULibraryBE.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private IAccountRepository AccountRepository => unitOfWork.AccountRepository;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AccountResponse> FindById(long id)
        {
            var response = await AccountRepository.FindByIdAsync(id);
            return response.Adapt<AccountResponse>();
        }

        public async Task<List<AccountResponse>> GetAll()
        {
            var list = await AccountRepository.GetAllAsync();
            var response = list.Adapt<List<AccountResponse>>();

            return response;
        }

        public async Task<AccountResponse> Add(AccountRequest request)
        {
            var account = request.Adapt<Account>();
            account.IsDeleted = false;

            var model = await AccountRepository.AddAsync(account);
            if (model is null) return null!;

            await unitOfWork.SaveChangeAsync();

            var response = await FindById(model.Id);
            return response;
        }

        public async Task<bool> Update(long id, AccountRequest request)
        {
            var model = await AccountRepository.FindByIdAsync(id);
            if (model is null) return false;

            request.Adapt(model);

            var response = AccountRepository.Update(model);
            await unitOfWork.SaveChangeAsync();

            return response;
        }

        public async Task<bool> Delete(long id)
        {
            var model = await AccountRepository.FindByIdAsync(id);
            if (model is null) return false;

            var response = AccountRepository.Delete(model);
            await unitOfWork.SaveChangeAsync();

            return response;
        }
    }
}
