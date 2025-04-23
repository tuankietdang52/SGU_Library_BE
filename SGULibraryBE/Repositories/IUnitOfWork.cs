namespace SGULibraryBE.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IBorrowDeviceRepository BorrowDeviceRepository { get; }

        Task SaveChangeAsync();
    }
}
