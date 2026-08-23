using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class MileageId : IEquatable<MileageId>
{
    public Guid Value { get; init; }

    private MileageId(Guid value)
    {
        Value = value;
    }

    public static ErrorOr<MileageId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("MileageId", "MileageId cannot be empty.");
        }

        return new MileageId(value);
    }

    public bool Equals(MileageId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as MileageId);
    public override int GetHashCode() => Value.GetHashCode();
}
