namespace Application.TestTemplates.Queries.GetTestTemplateById;

public class TestTemplateDto
{
    public TestTemplateDto()
    {
        Questions = new List<TestQuestionDto>();
        PossibleResults = new List<TestResultDto>();
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<TestQuestionDto> Questions { get; set; }

    public IEnumerable<TestResultDto> PossibleResults { get; set; }
}