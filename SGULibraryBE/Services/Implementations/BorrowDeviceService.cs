using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class BorrowDeviceService : IBorrowDeviceService
    {
        private readonly IUnitOfWork unitOfWork;
        private IBorrowDeviceRepository BorrowDeviceRepository => unitOfWork.BorrowDeviceRepository;
        private IAccountRepository AccountRepository => unitOfWork.AccountRepository;
        private IDeviceRepository DeviceRepository => unitOfWork.DeviceRepository;

        private readonly BorrowDeviceValidation validation = new();

        public BorrowDeviceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BorrowDeviceResponse>>> GetAll()
        {
            var response = await BorrowDeviceRepository.GetAllAsync();
            return Result<List<BorrowDeviceResponse>>.Success(response.Adapt<List<BorrowDeviceResponse>>());
        }

        public async Task<Result<BorrowDeviceResponse>> FindById(long id)
        {
            var response = await BorrowDeviceRepository.FindByIdAsync(id);

            if (response != null)
                return Result<BorrowDeviceResponse>.Success(response.Adapt<BorrowDeviceResponse>());
            else
                return Result<BorrowDeviceResponse>.Failure(Error.NotFound($"Borrow Device with id {id} does not exist"));
        }

        private bool IsAccountAndDeviceExist(long accountId, long deviceId)
        {
            var aResult = AccountRepository.FindByIdAsync(accountId).Result;
            var dResult = DeviceRepository.FindByIdAsync(deviceId).Result;

            return aResult is not null && dResult is not null;
        }

        public async Task<Result<BorrowDeviceResponse>> Add(BorrowDeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to add borrow device"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to add borrow device. User or Device does not exist"));
            }

            BorrowDevice model = request.Adapt<BorrowDevice>();
            model.IsDeleted = false;

            var response = await BorrowDeviceRepository.AddAsync(model);
            if (response is null)
            {
                return Result<BorrowDeviceResponse>.Failure(Error.Failure("Failed to add borrow device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result<BorrowDeviceResponse>.Success(response.Adapt<BorrowDeviceResponse>());
        }

        public async Task<Result> Update(long id, BorrowDeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to update borrow device"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to update borrow device. User or Device does not exist"));
            }

            var model = await BorrowDeviceRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Borrow Device with id {id} does not exist"));
            }

            request.Adapt(model);

            if (!BorrowDeviceRepository.Update(model))
            {
                return Result.Failure(Error.Failure("Failed to update device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }

        public async Task<Result> Delete(long id)
        {
            var model = await BorrowDeviceRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Borrow Device with id {id} does not exist"));
            }

            if (!BorrowDeviceRepository.Delete(model))
            {
                return Result.Failure(Error.Failure("Failed to delete borrow device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
    }
}
