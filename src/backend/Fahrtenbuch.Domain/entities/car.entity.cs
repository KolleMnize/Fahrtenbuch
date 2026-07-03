using ErrorOr;
using Fahrtenbuch.Domain.sharedkernel;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.entities;

internal class Car : Entity
{
    public CarId Id { get; init; }
    public string Name { get; init; }

    private Car(CarId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Car Create(CarId id, string name)
    {
        return new Car(id, name);
    }

    public static Car Rehydrate(CarId id, string name)
    {
        return new Car(id, name);
    }

}