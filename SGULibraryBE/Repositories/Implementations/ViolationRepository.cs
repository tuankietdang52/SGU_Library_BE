using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public class ViolationRepository : Repository<long, Violation>, IViolationRepository
    {
        public ViolationRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<Violation?> FindByIdAsync(long id)
        {
            return await _dbSet.IncludeAll(References)
                               .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
