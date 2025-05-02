namespace SGULibraryBE.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IBorrowDeviceRepository BorrowDeviceRepository { get; }
        IViolationRepository ViolationRepository { get; }
        IAccountViolationRepository AccountViolationRepository { get; }
        IReservationRepository ReservationRepository { get; }

        Task SaveChangeAsync();
    }
}
