using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    
    private readonly List<DomainEventBase> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEventBase domain)
    {
        _domainEvents.Add(domain);
    }

    public void RemoveDomainEvent(DomainEventBase domain)
    {
        _domainEvents.Remove(domain);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}