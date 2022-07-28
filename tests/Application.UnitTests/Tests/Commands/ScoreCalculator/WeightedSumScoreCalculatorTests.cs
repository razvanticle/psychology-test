using System.Collections;
using Application.Common.Exceptions;
using Application.Tests.Commands.ComputeTestResult.ScoreCalculator;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Tests.Commands.ScoreCalculator;

public class WeightedSumScoreCalculatorTests
{
    [TestCaseSource(nameof(TestCases))]
    public void WhenCalled_ReturnTheCorrectSum(IEnumerable<WeightedScoreInput> input, decimal expected)
    {
        // arrange
        var sut = new WeightedSumScoreCalculator();
       
        // act
        var actual = sut.Compute(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void WhenInvalidScoreInput_ThrowsInvalidScoreException()
    {
        // arrange
        var sut = new WeightedSumScoreCalculator();
        var input = new List<WeightedScoreInput>
        {
            new()
            {
                Score = 11,
                MaxScore = 10,
                Weight = 0.60m
            },
            new()
            {
                Score = 3,
                MaxScore = 10,
                Weight = 0.30m
            },
            new()
            {
                Score = 5,
                MaxScore = 10,
                Weight = 0.10m
            }
        };

        // act
        var act = () => sut.Compute(input);
        
        // assert
        act.Should().Throw<InvalidScoreException>()
            .WithMessage("Score 11 is invalid. Score must be between 0 and 10");
    }

    #region TestCases

    private static IEnumerable TestCases()
    {
        yield return new object[]
        {
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 1,
                    MaxScore = 5,
                    Weight = 0.25m
                },
                new()
                {
                    Score = 1,
                    MaxScore = 5,
                    Weight = 0.25m
                },
                new()
                {
                    Score = 1,
                    MaxScore = 5,
                    Weight = 0.25m
                },
                new()
                {
                    Score = 1,
                    MaxScore = 5,
                    Weight = 0.25m
                }
            },
            0.2m
        };
        yield return new object[]
        {
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 5,
                    MaxScore = 5,
                    Weight = 0.20m
                },
                new()
                {
                    Score = 3,
                    MaxScore = 5,
                    Weight = 0.30m
                },
                new()
                {
                    Score = 2,
                    MaxScore = 5,
                    Weight = 0.15m
                },
                new()
                {
                    Score = 4,
                    MaxScore = 5,
                    Weight = 0.35m
                }
            },
            0.72m
        };
        yield return new object[]
        {
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 9,
                    MaxScore = 10,
                    Weight = 0.60m
                },
                new()
                {
                    Score = 3,
                    MaxScore = 10,
                    Weight = 0.30m
                },
                new()
                {
                    Score = 5,
                    MaxScore = 10,
                    Weight = 0.10m
                }
            },
            0.68m
        };
    }

    #endregion
}