namespace EmployeeManagement.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ApiConstants.UnhandledExceptionLogMessage);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            InvalidOperationException => HttpStatusCode.BadRequest,
            BusinessRuleException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        object problemDetails = new
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = (int)statusCode
        };

        context.Response.ContentType = ApiConstants.JsonContentType;
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}
