using MediatR;

namespace Application.TestTemplates.Queries;

public class GetTestTemplatesQueryHandler : IRequestHandler<GetTestTemplatesQuery, TestTemplateDto>
{
    public Task<TestTemplateDto> Handle(GetTestTemplatesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new TestTemplateDto
        {
            Name = "Personality Test",
            Description = "This is a personality test"
        });
    }
}