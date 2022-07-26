namespace Application.Tests.Commands.ScoreCalculator;

public interface IScoreCalculator<in TInput>
{
    decimal Compute(TInput input);
}