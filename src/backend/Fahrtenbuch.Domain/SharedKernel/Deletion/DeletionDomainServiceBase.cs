using ErrorOr;

namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

internal abstract class DeletionDomainServiceBase<TId> : IDeletionDomainService<TId>
{
    internal protected abstract Task<ErrorOr<IReadOnlyList<DeletionBlockingReference>>> GetBlockingReasons(TId aggregateId, CancellationToken ct = default);

    public async Task<ErrorOr<Success>> EnsureDeletable(TId aggregateId, CancellationToken ct = default)
    {
        var reasons = await GetBlockingReasons(aggregateId, ct);
        if (reasons.IsError)
            return reasons.Errors;
        if (reasons.Value.Any())
            return reasons.Value.Select(
                r => Error.Conflict(
                    $"{r.BlockedAggregate.GetType().Name}.Conflict",
                    $"Aggregate {r.BlockedAggregate.GetType().Name} with Id {r.BlockedAggregate.Id.Value} " +
                    $"cant be deleted because it has blocking references to " +
                    $"aggregate {r.BlockingAggregate.GetType().Name} with Id {r.BlockingAggregate.Id.Value}")).ToList();

        return Result.Success;
    }
}
