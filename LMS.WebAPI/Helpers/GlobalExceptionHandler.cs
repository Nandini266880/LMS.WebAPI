using LMS.WebAPI.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Helpers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled error occurred.");
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;

            if (exception is UnauthorizedAccessException e1)
            {
                problemDetails.Title = e1.Message;
                problemDetails.Status = StatusCodes.Status403Forbidden;
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else if(exception is NotFoundException e2)
            {
                problemDetails.Title = e2.Message;
                problemDetails.Status = StatusCodes.Status404NotFound;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "An unexpected error occurred!";
                problemDetails.Detail = exception.Message;
            }

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
