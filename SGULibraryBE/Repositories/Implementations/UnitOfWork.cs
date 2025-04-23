
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

        public UnitOfWork(
            AppDbContext dbContext,
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository,
            IBorrowDeviceRepository borrowDeviceRepository)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
            DeviceRepository = deviceRepository;
            BorrowDeviceRepository = borrowDeviceRepository;
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
