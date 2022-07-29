using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Queries;

public record GetTestByIdQuery(int Id) : IRequest<TestResultDto>;

public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, TestResultDto>
{
    private readonly IMapper mapper;
    private readonly IRepository repository;

    public GetTestByIdQueryHandler(IRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<TestResultDto> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetEntities<TestResult>()
            .Where(x => x.Id == request.Id)
            .ProjectTo<TestResultDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            throw new EntityNotFoundException(nameof(TestTemplate), request.Id);
        }

        return result;
    }
}