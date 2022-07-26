namespace Application.Common.Exceptions;

public class InvalidScoreException : Exception
{
    public InvalidScoreException(string message)
        : base(message)
    {
    }

    public InvalidScoreException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InvalidScoreException(int score, decimal minScore, decimal maxScore)
        : base($"Score {score} is invalid. Score must be between {minScore} and {maxScore}")
    {
    }
}