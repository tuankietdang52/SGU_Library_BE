using SGULibraryBE.DTOs.Requests;

namespace SGULibraryBE.DTOs.Validation
{
    public class ReservationValidation : IRequestValidation<ReservationRequest>
    {
        public bool Validate(ReservationRequest request)
        {
            if (request.DateCreate == DateTime.MinValue) return false;
            if (request.DateBorrow == DateTime.MinValue) return false;
            if (request.DateReturn == DateTime.MinValue) return false;
            if (request.Quantity == 0) return false;

            return true;
        }
    }
}
