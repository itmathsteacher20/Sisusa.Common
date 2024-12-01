using System.Linq.Expressions;

namespace Sisusa.Common.Domain;

/// <summary>
/// Defines the contract for a data context that facilitates interactions with a data store.
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// Persists all changes made in the context to the data store asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the data store.</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Persists all changes made in the context to the data store asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the data store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Executes the given SQL command against the database in a raw format asynchronously.
    /// </summary>
    /// <param name="sql">The SQL command string to execute.</param>
    /// <param name="parameters">The parameters to apply to the SQL command string.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the number of rows affected.</returns>
    Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);

    /// <summary>
    /// Executes the given SQL command against the database in a raw format asynchronously.
    /// </summary>
    /// <param name="sql">The SQL command string to execute.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <param name="parameters">The parameters to apply to the SQL command string.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the number of rows affected.</returns>
    Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken, params object[] parameters);

    /// <summary>
    /// Retrieves a set of entities of the specified type from the data context.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the set.</typeparam>
    /// <returns>A set containing the entities of the specified type.</returns>
    ISet<T> Set<T>();
}

/// <summary>
/// Represents an exception that is thrown when an entity cannot be found in the data store.
/// </summary>
public sealed class EntityNotFoundException(
    string message = "No item with specified key value was found in the datastore. Operation cancelled.") :
    KeyNotFoundException(message: message)
{
    /// <summary>
    /// Throws an <see cref="EntityNotFoundException"/> for a specified key value.
    /// </summary>
    /// <typeparam name="T">The type of the key value.</typeparam>
    /// <param name="keyValue">The key value that was not found in the datastore.</param>
    /// <param name="paramName">The name of the parameter associated with the key value.</param>
    /// <returns>This method does not return a value as it always throws an exception.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when an entity with the specified key value is not found in the datastore.</exception>
    public static EntityNotFoundException ThrowFor<T>(T keyValue, string paramName)
    {
        var msg = $"Item with key value `{keyValue}` does not exist in the datastore.";
        if (!string.IsNullOrWhiteSpace(paramName))
        {
            msg = msg + $" Parameter name: `{paramName}`.";
        }
        throw new EntityNotFoundException(message: msg);
    }
};

/// <summary>
/// Provides a base implementation for a repository pattern to manage entity operations with a data store.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
/// <param name="dataContext">An instance of a data context used for data operations.</param>
/// <param name="createEntityStub">A function to create a stub for an entity given its identifier.</param>
/// <param name="getIdValue">A function to retrieve the identifier from an entity.</param>
/// <param name="haveEqualId">A function to determine if an entity's identifier matches the given identifier.</param>
public class RepositoryBase<T, TId>(
    IDataContext dataContext,
    Func<TId, T> createEntityStub,
    Func<T, TId> getIdValue,
    Func<T, TId, bool> haveEqualId)
    : IRepository<T, TId> where T : class
{
    public async Task<T?> FindByIdAsync(TId id)
    {
        var found = dataContext.Set<T>().Where(item => haveEqualId(item, id))
            .FirstOrDefault();
        throw new NotImplementedException();
    }

    public async Task<bool> HasByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<T>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<T>> FindAllByFilter(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountByFilterAsync(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task AddNewAsync(T entity)
    {
        throw new NotImplementedException();
    }
}