
using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models.Commons;

namespace SGULibraryBE.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IAccountRepository AccountRepository { get; }
        public IDeviceRepository DeviceRepository { get; }
        public IBorrowDeviceRepository BorrowDeviceRepository { get; }
        public IViolationRepository ViolationRepository { get; }
        public IAccountViolationRepository AccountViolationRepository { get; }
        public IReservationRepository ReservationRepository { get; }

        public UnitOfWork(
            AppDbContext dbContext,
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository,
            IBorrowDeviceRepository borrowDeviceRepository,
            IViolationRepository violationRepository,
            IAccountViolationRepository accountViolationRepository,
            IReservationRepository reservationRepository)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
            DeviceRepository = deviceRepository;
            BorrowDeviceRepository = borrowDeviceRepository;
            ViolationRepository = violationRepository;
            AccountViolationRepository = accountViolationRepository;
            ReservationRepository = reservationRepository;
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
