using ErrorOr;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Happenings;

/// <summary>
/// Repository interface for managing happening entities
/// </summary>
public interface IHappeningRepository : IBaseRepository<Happening, HappeningId>
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

    /// <summary>
    /// Checks if there are any happening entities for a specific mileage record.
    /// </summary>
    /// <param name="mileageId">The ID of the mileage record.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// True if there are happening entities for the mileage record, false otherwise, wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<bool>> ExistsForMileage(MileageId mileageId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all happening entities for a specific mileage record.
    /// </summary>
    /// <param name="mileageId">The ID of the mileage record.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A collection of happening entities for the mileage record wrapped in an ErrorOr result.
    /// Possible error types: <see cref="ErrorType.Unexpected"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<ErrorOr<IReadOnlyList<Happening>>> GetAllByMileage(MileageId mileageId, CancellationToken cancellationToken);
}