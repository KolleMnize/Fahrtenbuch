using ErrorOr;

namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

public abstract class DeletionDomainServiceBase<TAggregate, TId>
{
    protected abstract Task<IReadOnlyList<BlockingReason>> GetBlockingReasons(TAggregate aggregate);
    protected abstract Task DeleteFromRepository(TAggregate aggregate);

    public async Task<ErrorOr<Deleted>> Delete(TAggregate aggregate)
    {
        var reasons = await GetBlockingReasons(aggregate);
        if (reasons.Any())
            return reasons.Select(r => Error.Conflict($"{r.AggregateType}.Conflict", $"{r.AggregateId}: {r.Message}")).ToList();

        await DeleteFromRepository(aggregate);
        return Result.Deleted;
    }
}
