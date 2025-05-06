using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Utilities;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private IAccountRepository AccountRepository => unitOfWork.AccountRepository;
        private readonly AccountValidation validation = new();

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<AccountResponse>> FindById(long id)
        {
            var response = await AccountRepository.FindByIdAsync(id);

            if (response != null)
                return Result<AccountResponse>.Success(response.Adapt<AccountResponse>());
            else
                return Result<AccountResponse>.Failure(Error.NotFound($"Account with id {id} does not exist"));
        }

        public async Task<Result<List<AccountResponse>>> GetAll()
        {
            var list = await AccountRepository.GetAllAsync();
            var response = list.Adapt<List<AccountResponse>>();

            return Result<List<AccountResponse>>.Success(response);
        }

        public async Task<Result<AccountResponse>> Add(AccountRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<AccountResponse>.Failure(Error.Validation("Failed to add account"));
            }

            var account = request.Adapt<Account>();
            account.IsDeleted = false;

            var response = await AccountRepository.AddAsync(account);
            if (response is null)
            {
                return Result<AccountResponse>.Failure(Error.Failure("Failed to add account"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result<AccountResponse>.Success(response.Adapt<AccountResponse>());
        }

        public async Task<Result> Update(long id, AccountRequest request)
        {
            var model = await AccountRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.Failure($"Account with id {id} does not exist"));
            }

            if (!validation.Validate(request))
            {
                return Result.Failure(Error.Validation("Failed to add account"));
            }

            request.Adapt(model);

            if (!AccountRepository.Update(model))
            {
                return Result.Failure(Error.Failure($"Failed to update account with id {id}"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }

        public async Task<Result> Delete(long id)
        {
            var model = await AccountRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.Failure($"Account with id {id} does not exist"));
            }

            if (!AccountRepository.Delete(model))
            {
                return Result.Failure(Error.Failure($"Failed to delete account with id {id}"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }

        public async Task<Result<string>> SendMail(string email)
        {
            var account = await AccountRepository.FindByEmailAsync(email);
            if (account == null)
            {
                return Result<string>.Failure(Error.Failure($"Account with email {email} does not exist"));
            }

            string code = MailUtil.generateCodeOTP();
            DateTime expired = DateTime.Now.AddMinutes(5);
            MailUtil.SendEmail(email, "OTP Code", $"Your OTP code is: {code}. It will expire at {expired}");
            account.OTPCode = code;
            account.OTPExpired = expired;

            if (!AccountRepository.Update(account))
            {
                return Result<string>.Failure(Error.Failure($"Failed to update account with email {email}"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result<string>.Success(code);
        }

        public async Task<Result<string>> VerifyOtp(string email, string otp)
        {
            var account = await AccountRepository.FindByEmailAsync(email);
            if (account == null)
            {
                return Result<string>.Failure(Error.Failure($"Account with email {email} does not exist"));
            }
            string code = account.OTPCode;
            DateTime expired = (DateTime)account.OTPExpired;
            
            if (!code.Equals(otp) || MailUtil.isExpired(expired))
            {
                return Result<string>.Failure(Error.Failure($"OTP code is invalid or expired"));
            }

            return Result<string>.Success("Verify success");

        }
    }
}
