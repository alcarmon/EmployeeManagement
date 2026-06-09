namespace EmployeeManagement.API.Middleware;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        _logger.LogInformation(
            ApiConstants.RequestLogTemplate,
            request.Method,
            request.Path,
            request.QueryString,
            response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
