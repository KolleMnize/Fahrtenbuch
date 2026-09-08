using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
namespace Fahrtenbuch.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing mileage records.
/// </summary>
public interface IMileageRepository
{
    /// <summary>
    /// Creates a new mileage record in the repository.
    /// </summary>
    /// <param name="mileage">The mileage record to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A success result wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Success>> Create(Mileage mileage, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all mileage records from the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A list of all mileage records wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<IReadOnlyList<Mileage>>> GetAll(CancellationToken cancellationToken);
    /// <summary>
    /// Retrieves a mileage record by its ID.
    /// </summary>
    /// <param name="mileageId">The ID of the mileage record.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The mileage record wrapped in an ErrorOr result, or null if not found.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Mileage?>> GetById(MileageId mileageId, CancellationToken cancellationToken);
    /// <summary>
    /// Retrieves the following mileage record from a specific date for a given car.
    /// </summary>
    /// <param name="carId">The ID of the car.</param>
    /// <param name="date">The reference date.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The following mileage record wrapped in an ErrorOr result, or null if not found.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Mileage?>> GetFollowingMileageFromDate(CarId carId, DateTime date, CancellationToken cancellationToken);
    /// <summary>
    /// Retrieves the previous mileage record from a specific date for a given car.
    /// </summary>
    /// <param name="carId">The ID of the car.</param>
    /// <param name="date">The reference date.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The previous mileage record wrapped in an ErrorOr result, or null if not found.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<Mileage?>> GetPreviousMileageFromDate(CarId carId, DateTime date, CancellationToken cancellationToken);
    /// <summary>
    /// Checks if a mileage record exists by its ID.
    /// </summary>
    /// <param name="mileageId">The ID of the mileage record.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// True if the mileage record exists, false otherwise, wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<bool>> Exists(MileageId mileageId, CancellationToken cancellationToken);
}
