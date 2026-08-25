using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Aggregates;

public class Car : Aggregate
{
    public required CarId Id { get; init; }
    public required string Name { get; init; }

    private Car()
    {
        // Required for EF Core
    }
    private Car(CarId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ErrorOr<Car> Create(CarId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(
                code: "Car.InvalidName",
                description: "Car name cannot be empty."
            );
        }

        return new Car
        {
            Id = id,
            Name = name
        };
    }

}