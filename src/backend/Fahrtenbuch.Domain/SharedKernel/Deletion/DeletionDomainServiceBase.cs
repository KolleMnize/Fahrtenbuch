using ErrorOr;

namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

internal abstract class DeletionDomainServiceBase<TId> : IDeletionDomainService<TId>
{
    internal protected abstract Task<IReadOnlyList<BlockingReason>> GetBlockingReasons(TId aggregateId, CancellationToken ct = default);

    public async Task<ErrorOr<Success>> EnsureDeletable(TId aggregateId, CancellationToken ct = default)
    {
        var reasons = await GetBlockingReasons(aggregateId, ct);
        if (reasons.Any())
            return reasons.Select(r => Error.Conflict($"{r.AggregateType}.Conflict", $"{r.AggregateId}: {r.Message}")).ToList();

        return Result.Success;
    }
}
