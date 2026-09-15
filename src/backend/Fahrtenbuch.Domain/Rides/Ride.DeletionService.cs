using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel.Deletion;

namespace Fahrtenbuch.Domain.Rides;

internal class RideDeletionService : DeletionDomainServiceBase<RideId>, IRideDeletionService
{
    internal protected override Task<ErrorOr<IReadOnlyList<DeletionBlockingReference>>> GetBlockingReasons(RideId aggregateId, CancellationToken ct = default)
    {
        // Für Rides gibt es derzeit keine Blocking Reasons, daher wird eine leere Liste zurückgegeben.
        return Task.FromResult<ErrorOr<IReadOnlyList<DeletionBlockingReference>>>(new List<DeletionBlockingReference>());
    }
}