using Fahrtenbuch.Domain.sharedkernel;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.aggregates;

public class Mileage : Aggregate
{
    public MileageId Id { get; init; }
    public CarId CarId { get; init; }
    public decimal Value { get; init; }
    public DateTime Date { get; init; }

    private Mileage(MileageId id, CarId carId, decimal value, DateTime date)
    {
        Id = id;
        CarId = carId;
        Value = value;
        Date = date;
    }
    public static Mileage Create(MileageId id, CarId carId, decimal value, DateTime date)
    {
        return new Mileage(id, carId, value, date);
    }

    public static Mileage Rehydrate(MileageId id, CarId carId, decimal value, DateTime date)
    {
        return new Mileage(id, carId, value, date);

    }
}