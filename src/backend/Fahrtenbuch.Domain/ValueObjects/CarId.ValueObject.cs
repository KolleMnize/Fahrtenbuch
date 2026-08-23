using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class CarId : IEquatable<CarId>
{
    public Guid Value { get; init; }

    private CarId(Guid value)
    {
        Value = value;
    }

    public static ErrorOr<CarId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("CarId", "CarId cannot be empty.");
        }

        return new CarId(value);
    }
    public bool Equals(CarId? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as CarId);
    public override int GetHashCode() => Value.GetHashCode();
}
