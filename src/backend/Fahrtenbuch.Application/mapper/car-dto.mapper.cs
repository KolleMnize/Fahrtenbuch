using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Domain.aggregates;

namespace Fahrtenbuch.Application.mapper;

internal static class CarDtoMapper
{
    public static CarDto CarToCarDto(Car car)
        => new CarDto(car.Id.Value, car.Name);
}
