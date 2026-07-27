using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Application.mapper;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Domain.interfaces.repositories;

namespace Fahrtenbuch.Application.services;

public class CarManagementService(ICarRepository carRepository)
{
    public async Task Handle(CreateCarCommand command)
    {
        var car = Car.Create(CarId.Create(Guid.NewGuid()).Value, command.Name);
        carRepository.Create(car);
    }

    public async Task<GetCarsQueryResult> Handle(GetCarsQuery query)
    {
        IEnumerable<Car> repoResult = carRepository.GetAll();
        IEnumerable<CarDto> carDtos = repoResult.Select(CarDtoMapper.CarToCarDto);

        return new GetCarsQueryResult(carDtos);
    }
}