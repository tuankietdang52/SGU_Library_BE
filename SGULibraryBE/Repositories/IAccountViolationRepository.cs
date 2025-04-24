using SGULibraryBE.Models;

namespace SGULibraryBE.Repositories
{
    public interface IAccountViolationRepository : IRepository<long, AccountViolation>
    {
        public Task<List<AccountViolation>> FindByAccountIdAsync(long id);
        public Task<List<AccountViolation>> FindByViolationIdAsync(long id);
        public Task<AccountViolation?> IsAccountViolate(long accountId);
    }
}