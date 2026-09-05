using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Features.Cars.CreateCar;

public class CreateCarHandler(ICarRepository carRepository) : ICommandHandler<CreateCarCommand, CarDto>
{
    public async Task<ErrorOr<CarDto>> Handle(CreateCarCommand command, CancellationToken ct = default)
    {
        var carCreateResult = Car.Create(CarId.Create(Guid.NewGuid()).Value, command.Name);
        if (carCreateResult.IsError)
            return carCreateResult.Errors;

        var dbResult = await carRepository.Create(carCreateResult.Value, ct);
        if (dbResult.IsError)
            return dbResult.Errors;

        return CarDtoMapper.CarToCarDto(carCreateResult.Value);
    }
}