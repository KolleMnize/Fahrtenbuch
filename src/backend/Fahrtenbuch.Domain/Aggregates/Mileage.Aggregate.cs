using Fahrtenbuch.Domain.SharedKernel;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Aggregates;

public class Mileage : Aggregate
{
    public required MileageId Id { get; init; }
    public required CarId CarId { get; init; }
    public required decimal Value { get; init; }
    public required DateTime Date { get; init; }

    private Mileage()
    {
        // Required for EF Core
    }
    internal static Mileage Create(MileageId id, CarId carId, decimal value, DateTime date)
    {
        return new Mileage
        {
            Id = id,
            CarId = carId,
            Value = value,
            Date = date
        };
    }
}