using Application.Common.Exceptions;

namespace Application.Tests.Commands.ComputeTestResult.ScoreCalculator;

public class WeightedSumScoreCalculator : IScoreCalculator<IEnumerable<WeightedScoreInput>>
{
    public decimal Compute(IEnumerable<WeightedScoreInput> responses)
    {
        decimal sum = 0;
        foreach (var response in responses)
        {
            if (response.Score <= 0 || response.Score > response.MaxScore)
            {
                throw new InvalidScoreException(response.Score, 0, response.MaxScore);
            }

            sum += response.Score / response.MaxScore * response.Weight;
        }

        return sum;
    }
}