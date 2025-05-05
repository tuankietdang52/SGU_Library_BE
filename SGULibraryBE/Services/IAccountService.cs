using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Models;
using SGULibraryBE.Utilities.ResultHandler;
using System.Security.Cryptography;

namespace SGULibraryBE.Services
{
    public interface IAccountService
    {
        public Task<Result<List<AccountResponse>>> GetAll();
        public Task<Result<AccountResponse>> FindById(long id);
        public Task<Result<AccountResponse>> Add(AccountRequest request);
        public Task<Result> Update(long id, AccountRequest request);
        public Task<Result> Delete(long id);

        public Task<Result<string>> SendMail(string email);
        public Task<Result<string>> VerifyOtp(string email, string otp);

    }
}
