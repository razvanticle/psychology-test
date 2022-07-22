namespace Application.TestTemplates.Queries.GetTestTemplateById;

public class TestQuestionDto
{
    public TestQuestionDto()
    {
        Answers = new List<TestAnswerDto>();
    }

    public string Title { get; set; }

    public decimal Weight { get; set; }

    public IEnumerable<TestAnswerDto> Answers { get; set; }
}