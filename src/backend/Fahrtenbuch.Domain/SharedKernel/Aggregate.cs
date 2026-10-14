namespace Fahrtenbuch.Domain.SharedKernel;

public abstract class Aggregate<TId> : IAggregate
    where TId : AggregateId
{
    public required TId Id { get; init; }

    AggregateId IAggregate.Id => Id;
}
