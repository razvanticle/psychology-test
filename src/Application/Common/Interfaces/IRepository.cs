namespace Application.Common.Interfaces;

/// <summary>
///     Generic repository used for database read operations.
///     The entities retrieved through this repository cannot be modified and persisted back.
/// </summary>
public interface IRepository
{
    /// <summary>
    ///     Gets the entities from the database.
    /// </summary>
    IQueryable<T> GetEntities<T>() where T : class;
}