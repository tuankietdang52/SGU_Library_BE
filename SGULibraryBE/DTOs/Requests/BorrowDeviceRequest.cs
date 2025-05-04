namespace SGULibraryBE.DTOs.Requests
{
    public class BorrowDeviceRequest
    {
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateReturnExpected { get; set; }
        public DateTime? DateReturn { get; set; }
        public long UserId { get; set; }
        public long DeviceId { get; set; }
    }
}
