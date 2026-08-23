using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class RideId
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
}