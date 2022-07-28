using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Infrastructure.Persistence;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Dispatch(IReadOnlyCollection<BaseEntity> entities)
    {
        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}