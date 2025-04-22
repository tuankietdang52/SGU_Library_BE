using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Models;
using System.Security.Cryptography;

namespace SGULibraryBE.Services
{
    public interface IAccountService
    {
        public Task<List<AccountResponse>> GetAll();
        public Task<AccountResponse> FindById(long id);
        public Task<AccountResponse> Add(AccountRequest request);
        public Task<bool> Update(long id, AccountRequest request);
        public Task<bool> Delete(long id);
    }
}
