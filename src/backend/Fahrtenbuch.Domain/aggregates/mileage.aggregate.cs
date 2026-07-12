using Fahrtenbuch.Domain.sharedkernel;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.aggregates;

public class Mileage : Aggregate
{

    public MileageId Id { get; init; }
    public CarId CarId { get; init; }
    public decimal Value { get; init; }

    private Mileage(MileageId id, CarId carId, decimal value)
    {
        Id = id;
        CarId = carId;
        Value = value;
    }

    public static Mileage Create(MileageId id, CarId carId, decimal value)
    {
        return new Mileage(id, carId, value);
    }

    public static Mileage Rehydrate(MileageId id, CarId carId, decimal value)
    {
        return new Mileage(id, carId, value);

    }
}