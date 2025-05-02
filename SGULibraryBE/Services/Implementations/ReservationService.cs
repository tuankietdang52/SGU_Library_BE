using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Repositories.Implementations;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;
        private IReservationRepository ReservationRepository => unitOfWork.ReservationRepository;
        private IAccountRepository AccountRepository => unitOfWork.AccountRepository;
        private IDeviceRepository DeviceRepository => unitOfWork.DeviceRepository;

        private readonly ReservationValidation validation = new();

        public ReservationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<Result<List<ReservationResponse>>> FindByAccountId(long accountId)
        {
            var response = await ReservationRepository.FindByAccountId(accountId);
            return Result<List<ReservationResponse>>.Success(response.Adapt<List<ReservationResponse>>());
        }

        public async Task<Result<List<ReservationResponse>>> FindByDeviceId(long deviceId)
        {
            var response = await ReservationRepository.FindByDeviceId(deviceId);
            return Result<List<ReservationResponse>>.Success(response.Adapt<List<ReservationResponse>>());
        }

        public async Task<Result<ReservationResponse>> FindById(long id)
        {
            var response = await ReservationRepository.FindByIdAsync(id);

            if (response != null)
                return Result<ReservationResponse>.Success(response.Adapt<ReservationResponse>());
            else
                return Result<ReservationResponse>.Failure(Error.NotFound($"Reservation with id {id} does not exist"));
        }

        public async Task<Result<List<ReservationResponse>>> GetAll()
        {
            var response = await ReservationRepository.GetAllAsync();
            return Result<List<ReservationResponse>>.Success(response.Adapt<List<ReservationResponse>>());
        }

        private bool IsAccountAndDeviceExist(long accountId, long deviceId)
        {
            var aResult = AccountRepository.FindByIdAsync(accountId).Result;
            var dResult = DeviceRepository.FindByIdAsync(deviceId).Result;

            return aResult is not null && dResult is not null;
        }

        public async Task<Result<ReservationResponse>> Add(ReservationRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<ReservationResponse>.Failure(Error.BadRequest("Failed to add reservation"));
            }

            if (!IsAccountAndDeviceExist(request.UserId, request.DeviceId))
            {
                return Result<ReservationResponse>.Failure(Error.BadRequest("Failed to add reservation. User or Device does not exist"));
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
            return Result<ReservationResponse>.Success(response.Adapt<ReservationResponse>());
        }

        public async Task<Result> Update(long id, ReservationRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result.Failure(Error.BadRequest("Failed to update reservation"));
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
