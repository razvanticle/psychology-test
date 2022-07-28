using System.Collections;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Tests.Commands.ComputeTestResult;
using Application.Tests.Commands.ComputeTestResult.ScoreCalculator;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Tests.Commands;

public class ComputeTestResultCommandTests
{
    #region TestData

    private readonly TestTemplate personalityTest = new()
    {
        Id = 1,
        Questions =
        {
            new TestQuestion
            {
                Id = 1,
                Weight = 0.34m,
                Answers =
                {
                    new TestAnswer
                    {
                        Id = 1,
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Id = 2,
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Id = 3,
                        Score = 3
                    },
                    new TestAnswer
                    {
                        Id = 4,
                        Score = 4
                    }
                }
            },
            new TestQuestion
            {
                Id = 2,
                Weight = 0.33m,
                Answers =
                {
                    new TestAnswer
                    {
                        Id = 5,
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Id = 6,
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Id = 7,
                        Score = 3
                    },
                    new TestAnswer
                    {
                        Id = 8,
                        Score = 4
                    }
                }
            },
            new TestQuestion
            {
                Id = 3,
                Weight = 0.33m,
                Answers =
                {
                    new TestAnswer
                    {
                        Id = 9,
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Id = 10,
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Id = 11,
                        Score = 3
                    },
                    new TestAnswer
                    {
                        Id = 12,
                        Score = 4
                    }
                }
            }
        },
        PossibleResults =
        {
            new PossibleTestResult
            {
                Name = "Introvert",
                MinScore = 0.1m,
                MaxScore = 0.6m
            },
            new PossibleTestResult
            {
                Name = "Extrovert",
                MinScore = 0.7m,
                MaxScore = 1m
            }
        }
    };

    #endregion

    [TestCaseSource(nameof(IntrovertPersonalityTestCases))]
    [TestCaseSource(nameof(ExtrovertPersonalityTestCases))]
    public async Task WhenResponsesValid_ReturnTestResult(ComputeTestResultCommand command,
        List<WeightedScoreInput> scoreCalculatorInput, decimal score, string result)
    {
        // arrange
        var repositoryMock = new Mock<IRepository>();
        var templatesMock = new List<TestTemplate> { personalityTest }.BuildMock();
        repositoryMock.Setup(x => x.GetEntities<TestTemplate>())
            .Returns(templatesMock);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        
        var scoreCalculatorMock = new Mock<IScoreCalculator<IEnumerable<WeightedScoreInput>>>();
        scoreCalculatorMock.Setup(x => x.Compute(scoreCalculatorInput))
            .Returns(score);

        var sut = CreateSut(repositoryMock, unitOfWorkMock, scoreCalculatorMock);

        // act
        var actual = await sut.Handle(command, CancellationToken.None);

        // assert
        actual.Should().NotBeNull();
        actual.Result.Should().Be(result);
        scoreCalculatorMock.Verify(x => x.Compute(scoreCalculatorInput));
        unitOfWorkMock.Verify(x => x.Add(It.IsAny<TestResult>()));
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    private static ComputeTestResultCommandHandler CreateSut(Mock<IRepository>? repository = null,
        Mock<IUnitOfWork>? unitOfWork = null,
        Mock<IScoreCalculator<IEnumerable<WeightedScoreInput>>>? scoreCalculator = null)
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = mockMapper.CreateMapper();

        var sut = new ComputeTestResultCommandHandler(repository?.Object ?? Mock.Of<IRepository>(),
            unitOfWork?.Object ?? Mock.Of<IUnitOfWork>(),
            mapper,
            scoreCalculator?.Object ?? Mock.Of<IScoreCalculator<IEnumerable<WeightedScoreInput>>>());

        return sut;
    }

    #region TestCases

    private static IEnumerable IntrovertPersonalityTestCases()
    {
        yield return new object[]
        {
            new ComputeTestResultCommand
            {
                TestTemplateId = 1,
                Answers = new[]
                {
                    new QuestionAnswer
                    {
                        AnswerId = 2,
                        QuestionId = 1
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 6,
                        QuestionId = 2
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 10,
                        QuestionId = 3
                    }
                }
            },
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 2,
                    MaxScore = 4,
                    Weight = 0.34m
                },
                new()
                {
                    Score = 2,
                    MaxScore = 4,
                    Weight = 0.33m
                },
                new()
                {
                    Score = 2,
                    MaxScore = 4,
                    Weight = 0.33m
                }
            },
            0.5m,
            "Introvert"
        };
        yield return new object[]
        {
            new ComputeTestResultCommand
            {
                TestTemplateId = 1,
                Answers = new[]
                {
                    new QuestionAnswer
                    {
                        AnswerId = 1,
                        QuestionId = 1
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 5,
                        QuestionId = 2
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 9,
                        QuestionId = 3
                    }
                }
            },
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 1,
                    MaxScore = 4,
                    Weight = 0.34m
                },
                new()
                {
                    Score = 1,
                    MaxScore = 4,
                    Weight = 0.33m
                },
                new()
                {
                    Score = 1,
                    MaxScore = 4,
                    Weight = 0.33m
                }
            },
            0.25m,
            "Introvert"
        };
    }

    private static IEnumerable ExtrovertPersonalityTestCases()
    {
        yield return new object[]
        {
            new ComputeTestResultCommand
            {
                TestTemplateId = 1,
                Answers = new[]
                {
                    new QuestionAnswer
                    {
                        AnswerId = 3,
                        QuestionId = 1
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 7,
                        QuestionId = 2
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 11,
                        QuestionId = 3
                    }
                }
            },
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 3,
                    MaxScore = 4,
                    Weight = 0.34m
                },
                new()
                {
                    Score = 3,
                    MaxScore = 4,
                    Weight = 0.33m
                },
                new()
                {
                    Score = 3,
                    MaxScore = 4,
                    Weight = 0.33m
                }
            },
            0.75m,
            "Extrovert"
        };
        yield return new object[]
        {
            new ComputeTestResultCommand
            {
                TestTemplateId = 1,
                Answers = new[]
                {
                    new QuestionAnswer
                    {
                        AnswerId = 4,
                        QuestionId = 1
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 8,
                        QuestionId = 2
                    },
                    new QuestionAnswer
                    {
                        AnswerId = 12,
                        QuestionId = 3
                    }
                }
            },
            new List<WeightedScoreInput>
            {
                new()
                {
                    Score = 4,
                    MaxScore = 4,
                    Weight = 0.34m
                },
                new()
                {
                    Score = 4,
                    MaxScore = 4,
                    Weight = 0.33m
                },
                new()
                {
                    Score = 4,
                    MaxScore = 4,
                    Weight = 0.33m
                }
            },
            1m,
            "Extrovert"
        };
    }

    #endregion
}