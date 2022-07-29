using Domain.Entities;
using FluentValidation;

namespace Application.Tests.Commands.ComputeTestResult;

public class QuestionAnswerValidator : AbstractValidator<QuestionAnswer>
{
    public QuestionAnswerValidator()
    {
        RuleFor(x => x.AnswerId).NotNull().GreaterThan(0);
        RuleFor(x => x.QuestionId).NotNull().GreaterThan(0);
    }
}