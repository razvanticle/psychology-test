namespace Application.TestTemplates.Queries;

public class TestQuestionDto
{
    public TestQuestionDto()
    {
        Answers = new List<TestAnswerDto>();
    }

    public string Content { get; set; }

    public decimal Weight { get; set; }

    public IEnumerable<TestAnswerDto> Answers { get; }
}