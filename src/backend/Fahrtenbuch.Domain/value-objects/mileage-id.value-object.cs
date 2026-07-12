using ErrorOr;

namespace Fahrtenbuch.Domain.valueobjects;

public class MileageId
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
}
