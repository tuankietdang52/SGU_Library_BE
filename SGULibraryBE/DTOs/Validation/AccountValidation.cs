using SGULibraryBE.DTOs.Requests;

namespace SGULibraryBE.DTOs.Validation
{
    public class AccountValidation : IRequestValidation<AccountRequest>
    {
        public bool Validate(AccountRequest request)
        {
            if (request.StudentCode is null) return false;

            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Phone))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return false;
            }

            if (request.RoleId == 0) return false;

            return true;
        }
    }
}
