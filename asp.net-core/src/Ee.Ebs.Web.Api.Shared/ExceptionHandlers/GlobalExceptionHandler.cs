using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ee.Ebs.Web.Api.Shared.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Sistemde bir hata oluştu: {Message}", exception.Message);

        if (exception is ArgumentException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            var badRequestResponse = new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(badRequestResponse, cancellationToken);
            
            return true; 
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var serverErrorResponse = new
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = "Sunucu tarafında beklenmeyen bir hata oluştu."
        };

        await httpContext.Response.WriteAsJsonAsync(serverErrorResponse, cancellationToken);

        return true; 
    }
}