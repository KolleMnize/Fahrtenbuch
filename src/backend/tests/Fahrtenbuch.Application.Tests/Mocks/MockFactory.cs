using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Tests.Mocks;

public static class MockFactory
{
    public static Guid IdCar1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static Guid IdMileage1 = Guid.Parse("12222222-2222-2222-2222-222222222222");
    public static Car CreateMockedCar(
       Guid id = default,
       string name = "Mocked Car")
    {
        if (id == default)
        {
            id = IdCar1;
        }
        CarId carId = CarId.Create(id).Value;
        return Car.Create(carId, name);
    }

    // public static Mileage CreateMockedMileage(
    //     Guid id = default,
    //     Guid carId = default,
    //     decimal value = 500,
    //     DateTime date = default)
    // {
    //     if (id == default)
    //     {
    //         id = IdMileage1;
    //     }
    //     if (carId == default)
    //     {
    //         carId = IdCar1;
    //     }
    //     MileageId mileageId = MileageId.Create(id).Value;
    //     return Mileage.Create(mileageId, CarId.Create(carId).Value, value, date);
    // }
}