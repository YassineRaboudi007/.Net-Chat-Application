using ChatApplication.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Presentation.HandleErrorHelper
{
    public class ErrorHandlerController : ControllerBase, IErrorHandlerController
    {
        public IActionResult HandleFailure(Result result)
        {
            return result switch
            {
                { isSuccess: true } => throw new Exception("Operation succeded yet want to handle error"),
                IValidationResult validationResult =>
                    BadRequest(CreateProblemeDetails("ValidationError", StatusCodes.Status400BadRequest, result.error, validationResult.errors)),

                _ =>
                    BadRequest(CreateProblemeDetails("ValidationError", StatusCodes.Status400BadRequest, result.error)),
            };
        }

        private static ProblemDetails CreateProblemeDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null)
        {
            return new ProblemDetails()
            {
                Title = title,
                Status = status,
                Detail = error.Message,
                Extensions = { { nameof(error), errors } }
            };
        }
    }
}
