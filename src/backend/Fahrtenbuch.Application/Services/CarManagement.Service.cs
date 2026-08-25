using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Application.Mapper;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Domain.Interfaces.Repositories;

namespace Fahrtenbuch.Application.Services;

public class CarManagementService(ICarRepository carRepository)
{
    public async Task Handle(CreateCarCommand command)
    {
        var carCreateResult = Car.Create(CarId.Create(Guid.NewGuid()).Value, command.Name);
        if (carCreateResult.IsError)
        {
            throw new InvalidOperationException(carCreateResult.Errors[0].Description);
        }

        Car car = carCreateResult.Value;
        await carRepository.Create(car);
    }

    public async Task<GetCarsQueryResult> Handle(GetCarsQuery query)
    {
        IEnumerable<Car> repoResult = await carRepository.GetAll();
        IEnumerable<CarDto> carDtos = repoResult.Select(CarDtoMapper.CarToCarDto);

        return new GetCarsQueryResult(carDtos);
    }
}