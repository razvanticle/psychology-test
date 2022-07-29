using System.Net;

namespace WebAPI.ExceptionHandling.Handlers;

internal sealed class GenericExceptionHandler : IExceptionHandler<Exception>
{
    private readonly ILoggerFactory loggerFactory;
    private readonly IServiceProvider serviceProvider;

    public GenericExceptionHandler(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
    {
        this.serviceProvider = serviceProvider;
        this.loggerFactory = loggerFactory;
    }

    public ExceptionResponse Handle(Exception exception)
    {
        var logger = loggerFactory.CreateLogger(exception.Source);
        logger.LogError(default, exception, exception.Message);

        var handlerType = typeof(IExceptionHandler<>);
        var exceptionHandlerType = handlerType.MakeGenericType(exception.GetType());
        var handler = serviceProvider.GetService(exceptionHandlerType);

        if (IsNullOrGeneric(handler))
        {
            return new ExceptionResponse
            {
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Errors = new[]
                {
                    new ExceptionResponseItem
                    {
                        Code = ExceptionResponseCodes.Error,
                        Detail = exception.Message
                    }
                }
            };
        }

        var handleMethod = exceptionHandlerType.GetMethod("Handle");
        return (ExceptionResponse) handleMethod.Invoke(handler, new object[] {exception});
    }

    private bool IsNullOrGeneric(object handler) =>
        ReferenceEquals(null, handler) || ReferenceEquals(this, handler);
}