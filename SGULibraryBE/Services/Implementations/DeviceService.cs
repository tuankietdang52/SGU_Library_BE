using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.DTOs.Validation;
using SGULibraryBE.Models;
using SGULibraryBE.Repositories;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork unitOfWork;
        private IDeviceRepository DeviceRepository => unitOfWork.DeviceRepository;
        private readonly DeviceValidation validation = new();

        public DeviceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<DeviceResponse>> FindById(long id)
        {
            var response = await DeviceRepository.FindByIdAsync(id);

            if (response != null)
                return Result<DeviceResponse>.Success(response.Adapt<DeviceResponse>());
            else
                return Result<DeviceResponse>.Failure(Error.NotFound($"Device with id {id} does not exist"));
        }

        public async Task<Result<List<DeviceResponse>>> GetAll()
        {
            var response = await DeviceRepository.GetAllAsync();
            return Result<List<DeviceResponse>>.Success(response.Adapt<List<DeviceResponse>>());
        }

        public async Task<Result<DeviceResponse>> Add(DeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<DeviceResponse>.Failure(Error.BadRequest("Failed to add device"));
            }

            Device device = request.Adapt<Device>();
            device.IsDeleted = false;

            var response = await DeviceRepository.AddAsync(device);
            if (response is null)
            {
                return Result<DeviceResponse>.Failure(Error.Failure("Failed to add device"));
            }

            await unitOfWork.SaveChangeAsync();
            return await FindById(response.Id);
        }

        public async Task<Result> Update(long id, DeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result.Failure(Error.BadRequest("Failed to add device"));
            }

            var model = await DeviceRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Device with id {id} does not exist"));
            }

            request.Adapt(model);

            if (!DeviceRepository.Update(model))
            {
                return Result.Failure(Error.Failure("Failed to update device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }

        public async Task<Result> Delete(long id)
        {
            var model = await DeviceRepository.FindByIdAsync(id);
            if (model is null)
            {
                return Result.Failure(Error.NotFound($"Device with id {id} does not exist"));
            }

            if (!DeviceRepository.Delete(model))
            {
                return Result.Failure(Error.Failure("Failed to delete device"));
            }

            await unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
    }
}
