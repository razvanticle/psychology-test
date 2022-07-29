using WebAPI.ExceptionHandling.Handlers;

namespace WebAPI.ExceptionHandling;

public interface IExceptionHandler<in TException> where TException : Exception
{
    ExceptionResponse Handle(TException exception);
}