using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestTemplates.Queries.GetTestTemplateById;

public class GetTestTemplateByIdQueryHandler : IRequestHandler<GetTestTemplateByIdQuery, TestTemplateDto?>
{
    private readonly IMapper mapper;
    private readonly IRepository repository;

    public GetTestTemplateByIdQueryHandler(IRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<TestTemplateDto?> Handle(GetTestTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetEntities<TestTemplate>()
            .Where(x => x.Id == request.Id)
            .Include(x => x.Questions).ThenInclude(x => x.Answers)
            .Include(x => x.PossibleResults)
            .ProjectTo<TestTemplateDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(nameof(TestTemplate), request.Id);
        }

        return entity;
    }
}