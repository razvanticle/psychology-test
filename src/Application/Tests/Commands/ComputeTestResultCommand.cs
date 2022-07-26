using Application.Common.Dtos;
using MediatR;

namespace Application.Tests.Commands;

public record ComputeTestResultCommand : IRequest<TestResultDto>
{
    public int TestId { get; init; }

    public IEnumerable<TestResponseItem> TestResponses { get; init; } = new List<TestResponseItem>();
}