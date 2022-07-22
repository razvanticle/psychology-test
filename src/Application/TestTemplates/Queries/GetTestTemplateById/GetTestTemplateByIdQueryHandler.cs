using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestTemplates.Queries.GetTestTemplateById;

public class GetTestTemplateByIdQueryHandler : IRequestHandler<GetTestTemplateByIdQuery, TestTemplateDto?>
{
    private readonly IRepository repository;

    public GetTestTemplateByIdQueryHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public Task<TestTemplateDto?> Handle(GetTestTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = repository.GetEntities<TestTemplate>()
            .Where(x => x.Id == request.Id)
            .Include(x => x.Questions).ThenInclude(x => x.Answers)
            .Include(x => x.PossibleResults)
            .Select(x => new TestTemplateDto
            {
                Title = x.Title,
                Description = x.Description,
                Questions = x.Questions.Select(q => new TestQuestionDto
                {
                    Title = q.Title,
                    Weight = q.Weight,
                    Answers = q.Answers.Select(a => new TestAnswerDto
                    {
                        Content = a.Content,
                        Score = a.Score
                    })
                }),
                PossibleResults = x.PossibleResults.Select(r => new TestResultDto
                {
                    Name = r.Name,
                    MaxScore = r.MaxScore,
                    MinScore = r.MinScore
                })
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new Exception("entity not found.");

        return entity;
    }
}