namespace WebAPI.ExceptionHandling.Handlers;

public class ExceptionResponse
{
    public int StatusCode { get; set; }

    public IEnumerable<ExceptionResponseItem> Errors { get; set; }
}