namespace Fahrtenbuch.Domain.SharedKernel;

/// <summary>
/// Represents the unique identifier for an aggregate root in the domain.
/// </summary>
public abstract class AggregateId : IEquatable<AggregateId>
{
    public Guid Value { get; }

    protected AggregateId(Guid value) => Value = value;

    public bool Equals(AggregateId? other)
    {
        if (other is null) return false;
        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as AggregateId);
    public override int GetHashCode() => HashCode.Combine(GetType(), Value);
    public override string ToString() => Value.ToString();

}