namespace SGULibraryBE.DTOs.Responses
{
    public class DeviceResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
    }
}
