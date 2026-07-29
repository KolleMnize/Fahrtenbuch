using ErrorOr;

namespace Fahrtenbuch.Domain.ValueObjects;

public class HappeningId
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
            return Error.Validation("CarId", "CarId cannot be empty.");
        }

        return new HappeningId(value);
    }
}