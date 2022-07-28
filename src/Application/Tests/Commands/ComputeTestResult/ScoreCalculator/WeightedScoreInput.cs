namespace Application.Tests.Commands.ComputeTestResult.ScoreCalculator;

public record WeightedScoreInput
{
    public int Score { get; init; }

    public decimal MaxScore { get; init; }

    public decimal Weight { get; init; }
}