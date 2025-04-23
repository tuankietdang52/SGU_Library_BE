namespace SGULibraryBE.DTOs.Validation
{
    public interface IRequestValidation<T>
    {
        public bool Validate(T request);
    }
}