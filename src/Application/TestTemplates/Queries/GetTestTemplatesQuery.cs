using MediatR;

namespace Application.TestTemplates.Queries;

public record GetTestTemplatesQuery : IRequest<TestTemplateDto>;