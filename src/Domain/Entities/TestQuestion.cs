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
    public string Title { get; set; }

    /// <summary>
    ///     Represents how much does the question weight in the final test result calculation.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    ///     List of all possible answers.
    /// </summary>
    public IList<TestAnswer> Answers { get; }

    public TestTemplate TestTemplate { get; set; }

    public int TestTemplateId { get; set; }

    public decimal MaxScore => Answers.Select(x => x.Score).Max();
}