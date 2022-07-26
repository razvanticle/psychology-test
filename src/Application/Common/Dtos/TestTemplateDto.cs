namespace Application.Common.Dtos;

public class TestTemplateDto
{
    public TestTemplateDto()
    {
        Questions = new List<TestQuestionDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<TestQuestionDto> Questions { get; set; }
}