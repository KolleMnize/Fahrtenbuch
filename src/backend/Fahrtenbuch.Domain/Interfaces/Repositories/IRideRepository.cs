using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing ride entities.
/// </summary>
public interface IRideRepository
{
    /// <summary>
    /// Creates a new ride entity in the repository.
    /// </summary>
    /// <param name="ride">The ride entity to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// // <returns>
    /// A success result wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Success>> Create(Ride ride, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing ride entity in the repository.
    /// </summary>
    /// <param name="ride">The ride entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A success result wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Success>> Update(Ride ride, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all ride entities from the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A list of ride entities wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<IReadOnlyList<Ride>>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a ride entity by its unique identifier from the repository.
    /// </summary>
    /// <param name="rideId">The unique identifier of the ride entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// The ride entity wrapped in an ErrorOr result, or null if not found.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Ride?>> GetById(RideId rideId, CancellationToken cancellationToken);

    /// <summary>
    /// Checks if a ride entity exists in the repository by its unique identifier.
    /// </summary>
    /// <param name="rideId">The unique identifier of the ride entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A boolean value wrapped in an ErrorOr result indicating whether the ride exists.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<bool>> Exists(RideId rideId, CancellationToken cancellationToken);
}