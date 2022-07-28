using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence;

public interface IDatabaseSeeder
{
    Task SeedData(CancellationToken cancellationToken = default);
}

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly IUnitOfWork unitOfWork;

    public DatabaseSeeder(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task SeedData(CancellationToken cancellationToken = default)
    {
        var personalityTest = new TestTemplate
        {
            Title = "Are you an introvert or an extrovert?",
            Description = "Personality test that will show you if you are an introvert or an extrovert.",
            Questions =
            {
                new TestQuestion
                {
                    Title =
                        "You’re really busy at work and a colleague is telling you their life story and personal woes.",
                    Weight = 0.34m,
                    Answers =
                    {
                        new TestAnswer
                        {
                            Content = "Don’t dare to interrupt them",
                            Score = 1
                        },
                        new TestAnswer
                        {
                            Content = "Think it’s more important to give them some of your time; work can wait",
                            Score = 2
                        },
                        new TestAnswer
                        {
                            Content = "Listen, but with only with half an ear",
                            Score = 3
                        },
                        new TestAnswer
                        {
                            Content = "Interrupt and explain that you are really busy at the moment",
                            Score = 4
                        }
                    }
                },
                new TestQuestion
                {
                    Title =
                        "You’ve been sitting in the doctor’s waiting room for more than 25 minutes.",
                    Weight = 0.33m,
                    Answers =
                    {
                        new TestAnswer
                        {
                            Content = "Look at your watch every two minutes",
                            Score = 1
                        },
                        new TestAnswer
                        {
                            Content = "Bubble with inner anger, but keep quiet",
                            Score = 2
                        },
                        new TestAnswer
                        {
                            Content =
                                "Explain to other equally impatient people in the room that the doctor is always running late",
                            Score = 3
                        },
                        new TestAnswer
                        {
                            Content = "Complain in a loud voice, while tapping your foot impatiently",
                            Score = 4
                        }
                    }
                },
                new TestQuestion
                {
                    Title =
                        "You’re having an animated discussion with a colleague regarding a project that you’re in charge of.",
                    Weight = 0.33m,
                    Answers =
                    {
                        new TestAnswer
                        {
                            Content = "Don’t dare contradict them",
                            Score = 1
                        },
                        new TestAnswer
                        {
                            Content = "Think that they are obviously right",
                            Score = 2
                        },
                        new TestAnswer
                        {
                            Content = "Defend your own point of view, tooth and nail",
                            Score = 3
                        },
                        new TestAnswer
                        {
                            Content = "Continuously interrupt your colleague",
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
                    Description = "You are an extrovert.",
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            }
        };

        unitOfWork.Add(personalityTest);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }
}