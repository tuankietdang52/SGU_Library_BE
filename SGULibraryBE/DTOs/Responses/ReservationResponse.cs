namespace SGULibraryBE.DTOs.Responses
{
    public class ReservationResponse
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateReturn { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsDeleted { get; set; }
        public AccountResponse? User { get; set; }
        public DeviceResponse? Device { get; set; }
    }
}
