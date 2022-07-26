using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Tests.Commands.ScoreCalculator;
using Application.TestTemplates.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Commands;

public class ComputeTestResultCommandHandler : IRequestHandler<ComputeTestResultCommand, TestResultDto>
{
    private readonly IRepository repository;
    private readonly IMapper mapper;
    private readonly IScoreCalculator<IEnumerable<WeightedScoreInput>> calculator;

    public ComputeTestResultCommandHandler(IRepository repository, IMapper mapper, IScoreCalculator<IEnumerable<WeightedScoreInput>> calculator)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.calculator = calculator;
    }
    
    public async Task<TestResultDto> Handle(ComputeTestResultCommand request, CancellationToken cancellationToken)
    {
        var template = await repository.GetEntities<TestTemplate>()
            .Where(x => x.Id == request.TestId)
            .Include(x => x.Questions).ThenInclude(x => x.Answers)
            .Include(x => x.PossibleResults)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (template == null)
        {
            throw new EntityNotFoundException(nameof(TestTemplate), request.TestId);
        }
       
        var scoreCalculatorInputs = MapResponseToCalculatorInput(request.TestResponses, template);
        var sum = calculator.Compute(scoreCalculatorInputs);
        
        var result = template.PossibleResults.FirstOrDefault(x => sum >= x.MinScore && sum <= x.MaxScore);

        return mapper.Map<TestResultDto>(result);
    }

    private static IEnumerable<WeightedScoreInput> MapResponseToCalculatorInput(IEnumerable<TestResponseItem> testResponses, TestTemplate template)
    {
        var scoreCalculatorInputs = new List<WeightedScoreInput>();

        foreach (var response in testResponses)
        {
            var question = template.Questions.First(x => x.Id == response.QuestionId);
            var answer = question.Answers.First(x => x.Id == response.AnswerId);

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