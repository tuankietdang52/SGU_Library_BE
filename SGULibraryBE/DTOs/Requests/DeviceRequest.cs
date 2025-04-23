namespace SGULibraryBE.DTOs.Requests
{
    public class DeviceRequest
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
