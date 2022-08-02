using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Tests.Commands.ComputeTestResult.ScoreCalculator;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Commands.ComputeTestResult;

public class ComputeTestResultCommandHandler : IRequestHandler<ComputeTestResultCommand, TestResultDto>
{
    private readonly IRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IScoreCalculator<IEnumerable<WeightedScoreInput>> calculator;

    public ComputeTestResultCommandHandler(IRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IScoreCalculator<IEnumerable<WeightedScoreInput>> calculator)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.calculator = calculator;
    }
    
    public async Task<TestResultDto> Handle(ComputeTestResultCommand request, CancellationToken cancellationToken)
    {
        var template = await GetTestTemplate(request, cancellationToken);
        if (template == null)
        {
            throw new EntityNotFoundException(nameof(TestTemplate), request.TestTemplateId);
        }
        
        var score = ComputeScore(request.Answers, template);
        var scoreResult = template.GetResultForScore(score);
        var testResult = await CreateTestResult(request, score, scoreResult, cancellationToken);

        return mapper.Map<TestResultDto>(testResult);
    }

    private async Task<TestTemplate?> GetTestTemplate(ComputeTestResultCommand request, CancellationToken cancellationToken)
    {
        return await repository.GetEntities<TestTemplate>()
            .Where(x => x.Id == request.TestTemplateId)
            .Include(x => x.Questions).ThenInclude(x => x.Answers)
            .Include(x => x.PossibleResults)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private async Task<TestResult> CreateTestResult(ComputeTestResultCommand request,
        decimal score, PossibleTestResult result, CancellationToken cancellationToken)
    {
        var testResult = new TestResult
        {
            TestTemplateId = request.TestTemplateId,
            UserId = request.UserId,
            Answers = request.Answers,
            Score = score,
            Result = result.Name,
            Description = result.Description
        };

        testResult.AddDomainEvent(new TestResultCreatedEvent(testResult));
        await AddEntity(testResult, cancellationToken);
        
        return testResult;
    }

    private async Task AddEntity(TestResult testResult,CancellationToken cancellationToken)
    {
        unitOfWork.Add(testResult);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    // TODO: this should be extracted to a domain service
    private decimal ComputeScore(IEnumerable<QuestionAnswer> answers, TestTemplate template)
    {
        var scoreCalculatorInputs = MapResponseToCalculatorInput(answers, template);
        return calculator.Compute(scoreCalculatorInputs);
    }
    
    private static IEnumerable<WeightedScoreInput> MapResponseToCalculatorInput(IEnumerable<QuestionAnswer> answers, TestTemplate template)
    {
        var scoreCalculatorInputs = new List<WeightedScoreInput>();

        foreach (var questionAnswer in answers)
        {
            var question = template.Questions.First(x => x.Id == questionAnswer.QuestionId);
            var answer = question.Answers.First(x => x.Id == questionAnswer.AnswerId);

            var scoreCalculatorInput = new WeightedScoreInput
            {
                Score = answer.Score,
                MaxScore = question.MaxScore,
                Weight = question.Weight
            };

            scoreCalculatorInputs.Add(scoreCalculatorInput);
        }

        return scoreCalculatorInputs;
    }
}