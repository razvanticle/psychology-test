using Application.Common.Dtos;
using MediatR;

namespace Application.TestTemplates.Queries.GetTestTemplateById;

public record GetTestTemplateByIdQuery(int Id) : IRequest<TestTemplateDto?>;