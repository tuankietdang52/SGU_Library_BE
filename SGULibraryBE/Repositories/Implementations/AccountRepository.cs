using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;
using System.Security.Cryptography;

namespace SGULibraryBE.Repositories.Implementations
{
    public class AccountRepository : Repository<long, Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
            References.AddMultiple([
                e => e.Role!
            ]);
        }

        public override async Task<Account?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(item => item.StudentCode == id);
        }
    }
}