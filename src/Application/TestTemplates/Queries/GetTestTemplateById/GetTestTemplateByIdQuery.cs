using MediatR;

namespace Application.TestTemplates.Queries.GetTestTemplateById;

public record GetTestTemplateByIdQuery(int Id) : IRequest<TestTemplateDto?>;