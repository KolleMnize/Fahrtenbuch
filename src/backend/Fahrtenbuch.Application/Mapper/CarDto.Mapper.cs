using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Application.Mapper;

internal static class CarDtoMapper
{
    public static CarDto CarToCarDto(Car car)
        => new CarDto(car.Id.Value, car.Name);
}
