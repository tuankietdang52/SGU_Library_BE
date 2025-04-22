
using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;

namespace SGULibraryBE.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IAccountRepository AccountRepository { get; }

        public UnitOfWork(
            AppDbContext dbContext,
            IAccountRepository accountRepository)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
