using Domain.Entities;
using FluentValidation;

namespace Application.Tests.Commands.ComputeTestResult;

public class ComputeTestResultCommandValidator : AbstractValidator<ComputeTestResultCommand>
{
    public ComputeTestResultCommandValidator(IValidator<QuestionAnswer> questionAnswerValidator)
    {
        RuleFor(x => x.TestTemplateId).NotNull().GreaterThan(0);
        RuleForEach(x => x.Answers).SetValidator(questionAnswerValidator);
    }
}