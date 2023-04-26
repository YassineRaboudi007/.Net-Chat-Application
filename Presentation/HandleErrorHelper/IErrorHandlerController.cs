using ChatApplication.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Presentation.HandleErrorHelper
{
    public interface IErrorHandlerController
    {
        IActionResult HandleFailure(Result result);
    }
}
