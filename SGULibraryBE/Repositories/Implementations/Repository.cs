using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Repositories.Implementations
{
    public abstract class Repository<TId, TModel> : IRepository<TId, TModel> where TModel : BaseModel
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TModel> _dbSet;
        protected ReferenceCollection<TModel> References { get; }

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TModel>();
            References = [];
        }

        public abstract Task<TModel?> FindByIdAsync(TId id);

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            return await _dbSet.IncludeAll(References)
                               .Where(m => !m.IsDeleted)
                               .ToListAsync();
        }

        public virtual async Task<TModel?> AddAsync(TModel request)
        {
            try
            {
                var result = await _dbSet.AddAsync(request);
                return result.Entity;
            }
            catch
            {
                return null;
            }
        }

        public virtual bool Update(TModel request)
        {
            try
            {
                var result = _dbSet.Update(request);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(TModel request)
        {
            request.IsDeleted = true;
            return Update(request);
        }
    }
}
