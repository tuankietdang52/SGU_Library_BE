namespace SGULibraryBE.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }

        Task SaveChangeAsync();
    }
}
