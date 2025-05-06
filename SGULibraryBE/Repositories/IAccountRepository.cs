using SGULibraryBE.Models;

namespace SGULibraryBE.Repositories
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        Task<Account?> FindByEmailAsync(string email);
    }
}
