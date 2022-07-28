namespace Application.Tests.Commands.ComputeTestResult.ScoreCalculator;

public interface IScoreCalculator<in TInput>
{
    decimal Compute(TInput input);
}