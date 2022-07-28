using Domain.Common;

namespace Domain.Entities;

public class TestResult : AuditableEntity
{
    public int UserId { get; init; }

    public int TestTemplateId { get; init; }

    public TestTemplate TestTemplate { get; init; }

    public decimal Score { get; init; }

    public string Result { get; init; }

    public string Description { get; init; }

    public IEnumerable<QuestionAnswer> Answers { get; init; } = new List<QuestionAnswer>();
}