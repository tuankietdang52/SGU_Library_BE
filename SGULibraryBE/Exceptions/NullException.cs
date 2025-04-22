using System.Diagnostics.CodeAnalysis;

namespace SGULibraryBE.Exceptions
{
    public class NullException : Exception
    {
        public NullException() : base()
        {

        }

        public NullException(string message) : base(message)
        {
            
        }

        public static void ThrowIfNull([NotNull] object? value)
        {
            if (value is not null) return;
            
            string message = "Value is null";
            throw new NullException(message);
        }
    }
}
