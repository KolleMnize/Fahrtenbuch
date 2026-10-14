namespace Fahrtenbuch.Domain.SharedKernel;

public interface IAggregate
{
    public AggregateId Id { get; }
}