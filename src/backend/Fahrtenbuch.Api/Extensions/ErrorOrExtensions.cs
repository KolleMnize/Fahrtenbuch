using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Extensions;

public static class ErrorOrExtensions
{
    public static IActionResult ToProblemDetails<T>(this ErrorOr<T> result, ControllerBase controller)
    {
        if (!result.IsError)
            return controller.Ok(result.Value);

        var firstError = result.FirstError;

        var statusCode = firstError.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        if (result.Errors.Count > 1)
        {
            return controller.Problem(
                statusCode: statusCode,
                title: "Es sind mehrere Fehler aufgetreten",
                extensions: new Dictionary<string, object?>
                {
                    ["errors"] = result.Errors.Select(e => new { code = e.Code, message = e.Description })
                });
        }

        return controller.Problem(
            statusCode: statusCode,
            title: firstError.Code,
            detail: firstError.Description);
    }
}