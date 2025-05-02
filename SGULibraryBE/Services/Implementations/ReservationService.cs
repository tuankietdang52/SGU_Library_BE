using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Repositories.Implementations;
using SGULibraryBE.Utilities.ResultHandler;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGULibraryBE.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;
        private IReservationRepository ReservationRepository => unitOfWork.ReservationRepository;
        private IAccountRepository AccountRepository => unitOfWork.AccountRepository;
        private readonly IDeviceService _deviceService;

        private readonly ReservationValidation validation = new();

        public ReservationService(IUnitOfWork unitOfWork, IDeviceService deviceService)
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

        public async Task<Result<List<ReservationResponse>>> FindByAccountId(long accountId)
        {
            var list = await ReservationRepository.FindByAccountId(accountId);
            var response = list.Adapt<List<ReservationResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<ReservationResponse>>.Success(response);
        }

        public async Task<Result<List<ReservationResponse>>> FindByDeviceId(long deviceId)
        {
            var list = await ReservationRepository.FindByDeviceId(deviceId);
            var response = list.Adapt<List<ReservationResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<ReservationResponse>>.Success(response);
        }

        public async Task<Result<ReservationResponse>> FindById(long id)
        {
            var model = await ReservationRepository.FindByIdAsync(id);

            if (model != null)
            {
                var response = model.Adapt<ReservationResponse>();
                response.Device!.BorrowQuantity = GetDeviceBorrowQuantity(id);
                return Result<ReservationResponse>.Success(response);
            }
            else
                return Result<ReservationResponse>.Failure(Error.NotFound($"Reservation with id {id} does not exist"));
        }

        public async Task<Result<List<ReservationResponse>>> GetAll()
        {
            var list = await ReservationRepository.GetAllAsync();
            var response = list.Adapt<List<ReservationResponse>>();
            response.ForEach(item => item.Device!.BorrowQuantity = GetDeviceBorrowQuantity(item.Device.Id));

            return Result<List<ReservationResponse>>.Success(response);
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

        public async Task<Result<ReservationResponse>> Add(ReservationRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<ReservationResponse>.Failure(Error.Validation("Failed to add reservation"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result<ReservationResponse>.Failure(Error.BadRequest("Failed to add reservation. User or Device does not exist"));
            }

            if (!IsEnoughQuantity(request.DeviceId, request.Quantity))
            {
                return Result<ReservationResponse>.Failure(Error.BadRequest("Failed to add reservation. Not enough quantity"));
            }

            Reservation model = request.Adapt<Reservation>();
            model.IsDeleted = false;
            model.IsCheckedOut = false;

            var response = await ReservationRepository.AddAsync(model);
            if (response is null)
            {
                return Result<ReservationResponse>.Failure(Error.Failure("Failed to add reservation"));
            }

            await unitOfWork.SaveChangeAsync();

            int borrowQuantity = GetDeviceBorrowQuantity(response.DeviceId);
            if (borrowQuantity == -1)
                return Result<ReservationResponse>.Failure(Error.Failure("Failed to add reservation. Cant get borrow quantity"));

            var res = response.Adapt<ReservationResponse>();
            res.Device!.BorrowQuantity = borrowQuantity;

            return Result<ReservationResponse>.Success(res);
        }

        public async Task<Result> Update(long id, ReservationRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result.Failure(Error.Validation("Failed to update reservation"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result.Failure(Error.BadRequest("Failed to update reservation. User or Device does not exist"));
            }

            var model = await ReservationRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Reservation with id {id} does not exist"));
            }

            if (!IsEnoughQuantity(request.DeviceId, request.Quantity, model.Quantity))
            {
                return Result.Failure(Error.BadRequest("Failed to add reservation. Not enough quantity"));
            }

            request.Adapt(model);

            if (!ReservationRepository.Update(model))
            {
                return Result.Failure(Error.Failure("Failed to update device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }

        public async Task<Result> Delete(long id)
        {
            var model = await ReservationRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Reservation with id {id} does not exist"));
            }

            if (!ReservationRepository.Delete(model))
            {
                return Result.Failure(Error.Failure("Failed to delete reservation"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
    }
}
