using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Mileages;

public class MileageId : AggregateId
{
    private MileageId(Guid value) : base(value)
    { }

    public static ErrorOr<MileageId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Error.Validation("MileageId", "MileageId cannot be empty.");
        }

        return new MileageId(value);
    }
}
