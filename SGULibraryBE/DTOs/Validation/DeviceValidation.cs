using SGULibraryBE.DTOs.Requests;

namespace SGULibraryBE.DTOs.Validation
{
    public class DeviceValidation : IRequestValidation<DeviceRequest>
    {
        public bool Validate(DeviceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Image)) 
                return false;

            return true;
        }
    }
}
