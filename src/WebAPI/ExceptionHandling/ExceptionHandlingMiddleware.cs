namespace WebAPI.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext, IExceptionHandler<Exception> exceptionHandler)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            if (httpContext.Response.HasStarted) throw;

            var response = exceptionHandler.Handle(ex);
            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}