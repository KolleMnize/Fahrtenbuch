using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing car entities.
/// </summary>
public interface ICarRepository : IBaseRepository<Car, CarId>
{
    /// <summary>
    /// Checks if a car with the specified ID exists in the repository.
    /// </summary>
    /// <param name="carId">The ID of the car to check for existence.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A boolean value wrapped in an ErrorOr result indicating whether the car exists.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<bool>> Exists(CarId carId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new car in the repository.
    /// </summary>
    /// <param name="car">The car entity to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A success result wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Success>> Create(Car car, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all cars from the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// A list of all cars wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<IReadOnlyList<Car>>> GetAll(CancellationToken cancellationToken);
}