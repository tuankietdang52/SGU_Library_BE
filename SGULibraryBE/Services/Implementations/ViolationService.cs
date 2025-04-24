using Mapster;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Repositories;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Services.Implementations
{
    public class ViolationService : IViolationService
    {
        private readonly IUnitOfWork unitOfWork;
        private IViolationRepository ViolationRepository => unitOfWork.ViolationRepository;

        public ViolationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<ViolationResponse>> FindById(long id)
        {
            var response = await ViolationRepository.FindByIdAsync(id);

            if (response is null)
            {
                return Result<ViolationResponse>.Failure(Error.NotFound($"Violation with id {id} does not exist"));
            }

            return Result<ViolationResponse>.Success(response.Adapt<ViolationResponse>());
        }

        public async Task<Result<List<ViolationResponse>>> GetAll()
        {
            var response = await ViolationRepository.GetAllAsync();
            return Result<List<ViolationResponse>>.Success(response.Adapt<List<ViolationResponse>>());
        }
    }
}
