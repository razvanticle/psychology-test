using Domain.Common;

namespace Domain.Entities;

public class TestTemplate : AuditableEntity
{
    public TestTemplate()
    {
        Questions = new List<TestQuestion>();
        PossibleResults = new List<TestResult>();
    }

    /// <summary>
    ///     Name of the test.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Description of the test.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     List of all questions in the test.
    /// </summary>
    public IList<TestQuestion> Questions { get; }

    /// <summary>
    ///     The list of all possible results of the test.
    /// </summary>
    public IList<TestResult> PossibleResults { get; }
}