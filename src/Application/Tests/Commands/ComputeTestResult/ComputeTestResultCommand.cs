using Application.Common.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Tests.Commands.ComputeTestResult;

public record ComputeTestResultCommand : IRequest<TestResultDto>
{
    public int TestTemplateId { get; init; }

    public int UserId { get; init; }

    public IEnumerable<QuestionAnswer> Answers { get; init; } = new List<QuestionAnswer>();
}