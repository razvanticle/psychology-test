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
                    Description =
                        "You feel that living alone is to live happily, and you prefer hiding in a crowd rather than standing out in one. You are perpetually tormented by the idea of doing things wrong, not understanding or not being alert enough or intelligent enough to do what others expect of you. You lack in self-confidence and you seem to believe that others are better than you. While in a conversation, for example, you would be more likely to go along with the other’s points of view as you don’t fully respect your own opinions. Where there’s a low level task to complete or a service to be allotted, it’s you who volunteers. When people want to get out of a task, they naturally come to you as they know that you never say ‘no’.",
                    MinScore = 0.1m,
                    MaxScore = 0.6m
                },
                new PossibleTestResult
                {
                    Name = "Extrovert",
                    Description = "At first glance, people find it hard to understand how you could be so comfortable organising your private life and then seem to lose the better part of your self-confidence when you’re in public. Maybe it’s a question of rhythm? If you feel comfortable in your domestic rhythm it’s perhaps because it works more on a short-term basis (day, week or more rarely a month). On the contrary, the rhythm of professional life seems less concrete and more distant as the professional agenda works more to quarterly or annual plans, that you have no control over. Is this the source of your lessened motivation? You can’t control all the cards so you have to adapt and, in fact, that doesn’t interest you at all? If this difference between work and home life doesn’t bother you, then carry on. However, if you feel frustrated by this imbalance, it could be useful to look into the deeper reasons (either on your own or with someone else) that keep you less focused on professional ambition.",
                    MinScore = 0.6001m,
                    MaxScore = 1m
                }
            }
        };

        unitOfWork.Add(personalityTest);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }
}