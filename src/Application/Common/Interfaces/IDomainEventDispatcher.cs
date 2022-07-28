using Domain.Common;

namespace Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task Dispatch(IReadOnlyCollection<BaseEntity> entities);
}