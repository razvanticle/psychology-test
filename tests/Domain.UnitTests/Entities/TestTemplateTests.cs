using System.Collections;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.UnitTests.Entities;

public class TestTemplateTests
{
    [TestCaseSource(nameof(TestCases))]
    public void GetResultForScore_WhenCalled_ReturnTheCorrectResult(IList<PossibleTestResult> possibleTestResults, decimal score, int expectedId)
    {
        // arrange
        var sut = new TestTemplate
        {
            PossibleResults =possibleTestResults
        };

        // act
        var actual = sut.GetResultForScore(score);

        // assert
        actual.Id.Should().Be(expectedId);
    }
    
    [Test]
    public void GetResultForScore_WhenCalledWithInvalidScore_ThrowsTestResultNotFoundException()
    {
        // arrange
        var sut = new TestTemplate
        {
            PossibleResults =  new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            }
        };

        var score = 0.1m;
        
        // act
        var act = () =>  sut.GetResultForScore(score);
        
        // assert
        act.Should().Throw<TestResultNotFoundException>()
            .WithMessage($"Test result for score {score} was not found.");
    }

    private static IEnumerable TestCases()
    {
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.6578965422m,
            2
        };
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.6m,
            1
        };
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.60001m,
            1
        };
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.6001m,
            2
        };
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.6001001m,
            2
        };
        yield return new object[]
        {
            new List<PossibleTestResult>
            {
                new()
                {
                    Id = 1,
                    MinScore = 0.25m,
                    MaxScore = 0.6m,
                },
                new()
                {
                    Id = 2,
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            },
            0.599999999999999999m,
            1
        };
    }
}