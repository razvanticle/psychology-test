using Application.TestTemplates.Queries;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataAccess;

/// <inheritdoc />
public class Repository : IRepository
{
    private readonly ApplicationDbContext context;

    public Repository(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <inheritdoc />
    public IQueryable<T> GetEntities<T>() where T : class
    {
        return context.Set<T>().AsNoTracking();
    }
}