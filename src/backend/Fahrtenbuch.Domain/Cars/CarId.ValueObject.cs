using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Cars;

public class CarId : AggregateId
{
    private CarId(Guid value) : base(value)
    {
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
