using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing happening entities
/// </summary>
public interface IHappeningRepository
{
    /// <summary>
    /// Creates a new happening entity.
    /// </summary>
    /// <param name="happening">The happening entity to create.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A success result wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Success>> Create(Happening happening, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all happening entities.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A collection of happening entities wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<IReadOnlyList<Happening>>> GetAll(CancellationToken cancellationToken);
}