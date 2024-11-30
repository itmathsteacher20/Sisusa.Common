namespace Sisusa.Common.Domain;

/// <summary>
/// Extends the repository contract to include support for retrieving paginated collections of entities
/// of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of entity the repository manages.</typeparam>
/// <typeparam name="TId">The type of the identifier for the entity.</typeparam>
public interface IPaginatedRepository<T, in TId> : IRepository<T, TId> where T : class
{
    /// <summary>
    /// Retrieves a collection of all records with support for pagination.
    /// </summary>
    /// <param name="page">The current page number to retrieve.</param>
    /// <param name="pageSize">The number of records per page.</param>
    /// <returns>A collection of records for the specified page.</returns>
    Task<ICollection<T>> FindAllWithPagingAsync(int page, int pageSize);
}