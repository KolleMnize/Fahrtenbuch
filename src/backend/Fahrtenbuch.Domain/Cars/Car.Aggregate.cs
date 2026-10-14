using ErrorOr;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Cars;

public class Car : Aggregate<CarId>
{
    public required string Name { get; init; }

    private Car()
    {
        // Required for EF Core
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

        return new Car()
        {
            Id = id,
            Name = name
        };
    }

}