namespace SGULibraryBE.Utilities.ResultHandler
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;

        public ErrorType Type { get; set; }

        public Error(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }

        public static Error Failure(string message) => new(message, ErrorType.Failure);
        public static Error NotFound(string message) => new(message, ErrorType.NotFound);
        public static Error Validation(string message) => new(message, ErrorType.Validation);
        public static Error BadRequest(string message) => new(message, ErrorType.BadRequest);
    }
}
