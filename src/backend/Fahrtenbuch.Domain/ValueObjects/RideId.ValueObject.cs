using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class RideId : IEquatable<RideId>
{
    public Guid Value { get; init; }

    private RideId(Guid value)
    {
        Value = value;
    }

    public static ErrorOr<RideId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("RideId", "RideId cannot be empty.");
        }

        return new RideId(value);
    }

    public bool Equals(RideId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as RideId);
    public override int GetHashCode() => Value.GetHashCode();

}