using Domain.Common;

namespace Domain.Entities;

public class QuestionAnswer : BaseEntity
{
    public int QuestionId { get; init; }

    public int AnswerId { get; init; }
    
    public int TestResultId { get; init; }
    
    public TestResult TestResult { get; init; }
}