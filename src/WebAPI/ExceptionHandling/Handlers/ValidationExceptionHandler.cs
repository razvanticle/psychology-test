using System.Net;
using FluentValidation;

namespace WebAPI.ExceptionHandling.Handlers;

internal sealed class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public ExceptionResponse Handle(ValidationException exception)
    {
        var errors = exception.Errors.Select(x => new ExceptionResponseItem
        {
            Code = ExceptionResponseCodes.ValidationError,
            Detail = x.ErrorMessage
        });

        return new ExceptionResponse
        {
            StatusCode = (int) HttpStatusCode.BadRequest,
            Errors = errors
        };
    }
}