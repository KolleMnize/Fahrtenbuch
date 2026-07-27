using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public record CarId
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
}
