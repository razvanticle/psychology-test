using Domain.Common;

namespace Domain.Entities;

public class TestQuestion : AuditableEntity
{
    public TestQuestion()
    {
        Answers = new List<TestAnswer>();
    }

    /// <summary>
    ///     The content of the question.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Represents how much does the question weight in the final test result calculation.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    ///     List of all possible answers.
    /// </summary>
    public IEnumerable<TestAnswer> Answers { get; }
}