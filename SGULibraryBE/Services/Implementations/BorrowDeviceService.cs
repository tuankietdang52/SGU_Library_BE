using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Utilities.ResultHandler;
using System.Collections.Generic;

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

        private int GetDeviceBorrowQuantity(long deviceId)
        {
            var res = _deviceService.GetDeviceBorrowQuantity(deviceId).Result;

            if (!res.IsSuccess)
                return -1;
            else return res.Value;
        }

        public async Task<Result<List<BorrowDeviceResponse>>> GetAll()
        {
            var list = await BorrowDeviceRepository.GetAllAsync();
            var response = list.Adapt<List<BorrowDeviceResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<BorrowDeviceResponse>>.Success(response);
        }

        public async Task<Result<BorrowDeviceResponse>> FindById(long id)
        {
            var model = await BorrowDeviceRepository.FindByIdAsync(id);

            if (model != null)
            {
                var response = model.Adapt<BorrowDeviceResponse>();
                response.Device!.BorrowQuantity = GetDeviceBorrowQuantity(id);
                return Result<BorrowDeviceResponse>.Success(response);
            }
            else
                return Result<BorrowDeviceResponse>.Failure(Error.NotFound($"Borrow Device with id {id} does not exist"));
        }

        public async Task<Result<List<BorrowDeviceResponse>>> FindByAccountId(long accountId)
        {
            var list = await BorrowDeviceRepository.FindByAccountId(accountId);
            var response = list.Adapt<List<BorrowDeviceResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<BorrowDeviceResponse>>.Success(response);
        }

        public async Task<Result<List<BorrowDeviceResponse>>> FindByDeviceId(long deviceId)
        {
            var list = await BorrowDeviceRepository.FindByDeviceId(deviceId);
            var response = list.Adapt<List<BorrowDeviceResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<BorrowDeviceResponse>>.Success(response);
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

            int borrowQuantity = GetDeviceBorrowQuantity(response.DeviceId);
            if (borrowQuantity == -1)
                return Result<BorrowDeviceResponse>.Failure(Error.Failure("Failed to add borrow device. Cant get borrow quantity"));

            var res = response.Adapt<BorrowDeviceResponse>();
            res.Device!.BorrowQuantity = borrowQuantity;

            return Result<BorrowDeviceResponse>.Success(res);
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
