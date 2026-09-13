using Fahrtenbuch.Domain.SharedKernel.Deletion;

namespace Fahrtenbuch.Domain.Rides;

internal class RideDeletionService : DeletionDomainServiceBase<RideId>, IRideDeletionService
{
    internal protected override Task<IReadOnlyList<BlockingReason>> GetBlockingReasons(RideId aggregateId, CancellationToken ct = default)
    {
        // Für Rides gibt es derzeit keine Blocking Reasons, daher wird eine leere Liste zurückgegeben.
        return Task.FromResult((IReadOnlyList<BlockingReason>)Array.Empty<BlockingReason>());
    }
}