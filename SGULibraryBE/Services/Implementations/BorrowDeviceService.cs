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
        private readonly IDeviceService _deviceService;

        private readonly BorrowDeviceValidation validation = new();

        public BorrowDeviceService(IUnitOfWork unitOfWork, IDeviceService deviceService)
        {
            this.unitOfWork = unitOfWork;
            _deviceService = deviceService;
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

        public async Task<Result<List<BorrowDeviceResponse>>> FindByAccountId(long accountId)
        {
            var response = await BorrowDeviceRepository.FindByAccountId(accountId);
            return Result<List<BorrowDeviceResponse>>.Success(response.Adapt<List<BorrowDeviceResponse>>());
        }

        public async Task<Result<List<BorrowDeviceResponse>>> FindByDeviceId(long deviceId)
        {
            var response = await BorrowDeviceRepository.FindByDeviceId(deviceId);
            return Result<List<BorrowDeviceResponse>>.Success(response.Adapt<List<BorrowDeviceResponse>>());
        }

        private bool IsAccountAndDeviceExist(long accountId, long deviceId)
        {
            var aResult = AccountRepository.FindByIdAsync(accountId).Result;
            var dResult = _deviceService.FindById(deviceId).Result;

            return aResult is not null && dResult.IsSuccess;
        }

        private bool IsEnoughQuantity(long deviceId, int quantity)
        {
            var result = _deviceService.FindById(deviceId).Result;
            if (!result.IsSuccess) return false;

            var device = result.Value;
            int remain = device.Quantity - device.BorrowQuantity;

            return quantity <= remain;
        }

        private bool IsEnoughQuantity(long deviceId, int newQuantity, int oldQuantity)
        {
            var result = _deviceService.FindById(deviceId).Result;
            if (!result.IsSuccess) return false;

            var device = result.Value;
            int remain = device.Quantity - (device.BorrowQuantity - oldQuantity);

            return newQuantity <= remain;
        }

        public async Task<Result<BorrowDeviceResponse>> Add(BorrowDeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.Validation("Failed to add borrow device"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to add borrow device. User or Device does not exist"));
            }

            if (!IsEnoughQuantity(request.DeviceId, request.Quantity))
            {
                return Result<BorrowDeviceResponse>.Failure(Error.BadRequest("Failed to add borrow device. Not enough quantity"));
            }

            BorrowDevice model = request.Adapt<BorrowDevice>();
            model.IsDeleted = false;
            model.IsReturn = false;

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
                return Result.Failure(Error.Validation("Failed to update borrow device"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result.Failure(Error.BadRequest("Failed to update borrow device. User or Device does not exist"));
            }

            var model = await BorrowDeviceRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Borrow Device with id {id} does not exist"));
            }

            if (!IsEnoughQuantity(request.DeviceId, request.Quantity, model.Quantity))
            {
                return Result.Failure(Error.BadRequest("Failed to add borrow device. Not enough quantity"));
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
