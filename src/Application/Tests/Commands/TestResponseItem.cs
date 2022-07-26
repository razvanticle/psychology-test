namespace Application.Tests.Commands;

public record TestResponseItem
{
    public int QuestionId { get; init; }

    public int AnswerId { get; init; }
}