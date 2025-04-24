namespace SGULibraryBE.DTOs.Responses
{
    public class ViolationResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
