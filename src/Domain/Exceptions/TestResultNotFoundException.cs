namespace Domain.Exceptions;

public class TestResultNotFoundException : Exception
{
    public TestResultNotFoundException(string message)
        : base(message)
    {
    }

    public TestResultNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public TestResultNotFoundException(decimal score)
        : base($"Test result for score {score} was not found.")
    {
    }
}