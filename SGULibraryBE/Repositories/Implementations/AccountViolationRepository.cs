using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public class AccountViolationRepository : Repository<long, AccountViolation>, IAccountViolationRepository
    {
        public AccountViolationRepository(AppDbContext dbContext) : base(dbContext)
        {
            References.AddMultiple([
                e => e.User!,
                e => e.Violation!,
                e => e.User!.Role!
            ]);
        }

        public override async Task<AccountViolation?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<List<AccountViolation>> FindByAccountIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .Where(item => item.UserId == id)
                               .ToListAsync();
        }

        public async Task<List<AccountViolation>> FindByViolationIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .Where(item => item.ViolationId == id)
                               .ToListAsync();
        }

        public async Task<AccountViolation?> IsAccountViolate(long accountId)
        {
            return await _dbSet.IncludeAll(References)
                               .Where(item => item.UserId == accountId && !item.IsDeleted)
                               .FirstOrDefaultAsync();
        }
    }
}
