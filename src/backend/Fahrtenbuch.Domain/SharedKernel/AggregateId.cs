namespace Fahrtenbuch.Domain.SharedKernel;

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