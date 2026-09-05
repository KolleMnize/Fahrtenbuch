using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Application.Features.Cars;

internal static class CarDtoMapper
{
    public static CarDto CarToCarDto(Car car)
        => new CarDto(car.Id.Value, car.Name);
}
