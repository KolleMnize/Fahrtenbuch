using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Happenings;

public class HappeningId : AggregateId
{
    private HappeningId(Guid value) : base(value)
    { }

    public static ErrorOr<HappeningId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("HappeningId", "HappeningId cannot be empty.");
        }

        return new HappeningId(value);
    }
}