using ErrorOr;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

/// <summary>
/// Represents a generic base repository interface for CRUD operations.
/// </summary>
public interface IBaseRepository<TEntity, TId>
{
    /// <summary>
    /// Deletes an aggregate by its Id.
    /// </summary>
    /// <param name="id">The Id of the aggregate to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>
    /// A created result wrapped in an ErrorOr result indicating whether the Aggregate was successfully deleted.
    /// Possible error types: <see cref="ErrorType.Unexpected"/> , 
    /// <see cref="ErrorType.NotFound"/> ,
    /// <see cref="ErrorType.Conflict"/>
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    public Task<ErrorOr<Deleted>> Delete(TId id, CancellationToken cancellationToken);
}
