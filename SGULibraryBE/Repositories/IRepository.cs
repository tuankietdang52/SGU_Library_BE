using SGULibraryBE.Models.Commons;

namespace SGULibraryBE.Repositories
{
    public interface IRepository<TId, TModel> where TModel : BaseModel
    {
        public Task<List<TModel>> GetAllAsync();
        public Task<TModel?> FindByIdAsync(TId id);
        public Task<TModel?> AddAsync(TModel request);
        public bool Update(TModel request);
        public bool Delete(TModel request);
    }
}