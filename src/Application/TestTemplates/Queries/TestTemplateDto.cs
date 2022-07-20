namespace Application.TestTemplates.Queries;

public class TestTemplateDto
{
    public TestTemplateDto()
    {
        Questions = new List<TestQuestionDto>();
        PossibleResults = new List<TestResultDto>();
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<TestQuestionDto> Questions { get; }

    public IEnumerable<TestResultDto> PossibleResults { get; }
}