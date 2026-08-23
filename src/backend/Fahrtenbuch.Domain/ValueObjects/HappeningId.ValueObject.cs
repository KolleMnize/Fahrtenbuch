using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class HappeningId : IEquatable<HappeningId>
{
    public Guid Value { get; init; }

    private HappeningId(Guid value)
    {
        Value = value;
    }

    public static ErrorOr<HappeningId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("HappeningId", "HappeningId cannot be empty.");
        }

        return new HappeningId(value);
    }
    public bool Equals(HappeningId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as HappeningId);
    public override int GetHashCode() => Value.GetHashCode();
}