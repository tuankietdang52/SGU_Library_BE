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
            var model = await DeviceRepository.FindByIdAsync(id);

            if (model == null)
                return Result<DeviceResponse>.Failure(Error.NotFound($"Device with id {id} does not exist"));

            var response = model.Adapt<DeviceResponse>();
            var borrowQuantity = await DeviceRepository.GetDeviceBorrowQuantity(model.Id);

            response.BorrowQuantity = borrowQuantity;
            return Result<DeviceResponse>.Success(response);
        }

        public async Task<Result<int>> GetDeviceBorrowQuantity(long id)
        {
            var borrowQuantity = await DeviceRepository.GetDeviceBorrowQuantity(id);

            if (borrowQuantity == -1)
                return Result<int>.Failure(Error.NotFound($"Cant found device with Id {id}"));

            return Result<int>.Success(borrowQuantity);
        }

        public async Task<Result<List<DeviceResponse>>> GetAll()
        {
            var list = await DeviceRepository.GetDevicesWithBorrowQuantity();
            List<DeviceResponse> response = [.. list.Select(pr =>
            {
                DeviceResponse deviceResponse = pr.First.Adapt<DeviceResponse>();
                deviceResponse.BorrowQuantity = pr.Last;

                return deviceResponse;
            })];

            return Result<List<DeviceResponse>>.Success(response);
        }

        public async Task<Result<DeviceResponse>> Add(DeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result<DeviceResponse>.Failure(Error.Validation("Failed to add device"));
            }

            Device device = request.Adapt<Device>();
            device.IsDeleted = false;

            var response = await DeviceRepository.AddAsync(device);
            if (response is null)
            {
                return Result<DeviceResponse>.Failure(Error.Failure("Failed to add device"));
            }

            await unitOfWork.SaveChangeAsync();

            var value = response.Adapt<DeviceResponse>();
            value.BorrowQuantity = 0;

            return Result<DeviceResponse>.Success(value);
        }

        public async Task<Result> Update(long id, DeviceRequest request)
        {
            if (!validation.Validate(request))
            {
                return Result.Failure(Error.Validation("Failed to add device"));
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
