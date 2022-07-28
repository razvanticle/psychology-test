using System.Runtime.InteropServices;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities;

public class TestTemplate : AuditableEntity
{
    public TestTemplate()
    {
        Questions = new List<TestQuestion>();
        PossibleResults = new List<PossibleTestResult>();
    }

    /// <summary>
    ///     Name of the test.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    ///     Description of the test.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    ///     List of all questions in the test.
    /// </summary>
    public IList<TestQuestion> Questions { get; init; }

    /// <summary>
    ///     The list of all possible results of the test.
    /// </summary>
    public IList<PossibleTestResult> PossibleResults { get; init; }

    public PossibleTestResult GetResultForScore(decimal score)
    {
        var roundedScore = decimal.Round(score, 4);
        var result = PossibleResults.FirstOrDefault(x =>
            roundedScore >= decimal.Round(x.MinScore, 4) && roundedScore <= decimal.Round(x.MaxScore, 4));

        if (result == null)
        {
            throw new TestResultNotFoundException(score);
        }

        return result;
    }
}