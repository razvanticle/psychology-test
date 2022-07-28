using Application.Common.Interfaces;
using Domain.Common;

namespace Infrastructure.Persistence.DataAccess;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    private readonly IDomainEventDispatcher domainEventDispatcher;

    public UnitOfWork(ApplicationDbContext context, IDomainEventDispatcher domainEventDispatcher)
    {
        this.context = context;
        this.domainEventDispatcher = domainEventDispatcher;
    }

    /// <inheritdoc />
    public IQueryable<T> GetEntities<T>() where T : class
    {
        return context.Set<T>();
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var saveResult = await context.SaveChangesAsync(cancellationToken);
        
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        await domainEventDispatcher.Dispatch(entities);
        
        return saveResult;
    }

    /// <inheritdoc />
    public void Add<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Add(entity);
    }

    /// <inheritdoc />
    public void Update<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Update(entity);
    }

    /// <inheritdoc />
    public void Delete<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Remove(entity);
    }
}