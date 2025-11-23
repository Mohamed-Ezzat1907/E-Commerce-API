using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using ValidationError = Shared.ErrorModels.ValidationError;

namespace E_Commerce.Api.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context) 
        {
            // context ==> errors , key[Field] , status code
            // context.Modelsate ==> get errors from model state<string , ModelStateEntry>
            // string ==> Name of the field
            // ModelStateEntry ==> contains errors for that field d==> Error Messages
            // IEnumerable <ValidationError>

            var errors = context.ModelState.Where(error =>
            error.Value?.Errors.Any() == true).Select(error =>
            new ValidationError()
            {
                Field = error.Key,
                Errors = error.Value?.Errors.Select(e => e.ErrorMessage) ?? new List<string>()
            });

            var response = new ValidationErrorResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One or more validation errors occurred.",
                Errors = errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}
