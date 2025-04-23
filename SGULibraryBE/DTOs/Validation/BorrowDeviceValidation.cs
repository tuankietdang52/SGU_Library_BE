using SGULibraryBE.DTOs.Requests;

namespace SGULibraryBE.DTOs.Validation
{
    public class BorrowDeviceValidation : IRequestValidation<BorrowDeviceRequest>
    {
        public bool Validate(BorrowDeviceRequest request)
        {
            if (request.DateCreate == DateTime.MinValue) return false;
            if (request.DateBorrow == DateTime.MinValue) return false;
            if (request.DateReturn == DateTime.MinValue) return false;

            return true;
        }
    }
}
