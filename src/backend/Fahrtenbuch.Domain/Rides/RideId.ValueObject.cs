using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Rides;

public class RideId : AggregateId
{
    private RideId(Guid value) : base(value)
    { }

    public static ErrorOr<RideId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("RideId", "RideId cannot be empty.");
        }

        return new RideId(value);
    }
}