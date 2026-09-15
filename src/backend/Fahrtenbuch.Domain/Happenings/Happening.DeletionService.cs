using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel.Deletion;

namespace Fahrtenbuch.Domain.Happenings;

internal class HappeningDeletionService : DeletionDomainServiceBase<HappeningId>, IHappeningDeletionService
{
    protected internal override Task<ErrorOr<IReadOnlyList<BlockingReason>>> GetBlockingReasons(HappeningId aggregateId, CancellationToken ct = default)
    {
        /// Für Happenings gibt es derzeit keine Blocking Reasons, daher wird eine leere Liste zurückgegeben.
        return Task.FromResult<ErrorOr<IReadOnlyList<BlockingReason>>>(new List<BlockingReason>());
    }
}