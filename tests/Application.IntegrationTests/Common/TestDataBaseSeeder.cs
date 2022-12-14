using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Application.IntegrationTests.Common;

public class TestDataBaseSeeder : IDatabaseSeeder
{
    public static readonly TestTemplate PersonalityTest = new()
    {
        Title = "Personality test",
        Description = "Personality test description",
        Questions =
        {
            new TestQuestion
            {
                Title =
                    "First question",
                Weight = 0.34m,
                Answers =
                {
                    new TestAnswer
                    {
                        Content = "First question answer 1",
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Content = "First question answer 2",
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Content = "First question answer 3",
                        Score = 3
                    },
                    new TestAnswer
                    {
                        Content = "First question answer 4",
                        Score = 4
                    }
                }
            },
            new TestQuestion
            {
                Title =
                    "Second question",
                Weight = 0.33m,
                Answers =
                {
                    new TestAnswer
                    {
                        Content = "Second question answer 1",
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Content = "Second question answer 2",
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Content =
                            "Second question answer 3",
                        Score = 3
                    }
                }
            },
            new TestQuestion
            {
                Title =
                    "Third question",
                Weight = 0.33m,
                Answers =
                {
                    new TestAnswer
                    {
                        Content = "Third question answer 1",
                        Score = 1
                    },
                    new TestAnswer
                    {
                        Content = "Third question answer 2",
                        Score = 2
                    },
                    new TestAnswer
                    {
                        Content = "Third question answer 3",
                        Score = 3
                    },
                    new TestAnswer
                    {
                        Content = "Third question answer 4",
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
                Description = "You are an introvert.",
                MinScore = 0.1m,
                MaxScore = 0.6m
            },
            new PossibleTestResult
            {
                Name = "Extrovert",
                Description = "You are an introvert.",
                MinScore = 0.6001m,
                MaxScore = 1m
            }
        }
    };

    private readonly IUnitOfWork unitOfWork;

    public TestDataBaseSeeder(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task SeedData(CancellationToken cancellationToken = default)
    {
        unitOfWork.Add(PersonalityTest);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }
}