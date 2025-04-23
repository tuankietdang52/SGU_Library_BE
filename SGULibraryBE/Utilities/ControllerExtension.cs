using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Utilities.ResultHandler;

namespace SGULibraryBE.Utilities
{
    public static class ControllerExtension
    {
        public static IActionResult Response(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess) return controller.NoContent();

            string errorMessage = result.Error.Message;
            return result.Error.Type switch
            {
                ErrorType.Failure => controller.Problem(errorMessage),
                ErrorType.NotFound => controller.NotFound(errorMessage),
                ErrorType.Validation => controller.ValidationProblem(errorMessage),
                ErrorType.BadRequest => controller.BadRequest(errorMessage),
                _ => controller.Problem(),
            };
        }

        public static IActionResult Response<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess) return controller.Ok(result.Value);

            string errorMessage = result.Error.Message;
            return result.Error.Type switch
            {
                ErrorType.Failure => controller.Problem(errorMessage),
                ErrorType.NotFound => controller.NotFound(errorMessage),
                ErrorType.Validation => controller.ValidationProblem(errorMessage),
                ErrorType.BadRequest => controller.BadRequest(errorMessage),
                _ => controller.Problem(),
            };
        }
    }
}
