using Application.TestTemplates.Queries;
using Domain.Common;

namespace Application.Common.Interfaces;

/// <summary>
///     Unit of work used for inserting or updating the entities in tha database.
/// </summary>
public interface IUnitOfWork : IRepository
{
    /// <summary>
    ///     Saves the changes to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a new entity to the current unit of work. The changes are persisted only when <see cref="SaveChangesAsync" />
    ///     is called.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <typeparam name="T">Entity type</typeparam>
    void Add<T>(T entity) where T : BaseEntity;

    /// <summary>
    ///     Updated an entity from the current unit of work. The changes are persisted only when
    ///     <see cref="SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="entity">The entity to be updated..</param>
    /// <typeparam name="T">Entity type</typeparam>
    void Update<T>(T entity) where T : BaseEntity;

    /// <summary>
    ///     Deletes an entity from the current unit of work. The changes are persisted only when
    ///     <see cref="SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <typeparam name="T">Entity type</typeparam>
    void Delete<T>(T entity) where T : BaseEntity;
}