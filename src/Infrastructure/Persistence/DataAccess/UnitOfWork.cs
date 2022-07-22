using Application.Common.Interfaces;
using Domain.Common;

namespace Infrastructure.Persistence.DataAccess;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <inheritdoc />
    public IQueryable<T> GetEntities<T>() where T : class
    {
        return context.Set<T>();
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public void Add<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Add(entity);
    }

    public void Update<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Update(entity);
    }

    public void Delete<T>(T entity) where T : BaseEntity
    {
        context.Set<T>().Remove(entity);
    }
}