using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Application.mapper;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.records;
using Fahrtenbuch.Infrastructure.services;

namespace Fahrtenbuch.Application.services;

public class CarManagementService(RepositoryService repositoryService)
{
    public async Task Handle(CreateCarCommand command)
    {
        var car = Car.Create(CarId.Create(Guid.NewGuid()).Value, command.Name);
        repositoryService.CarRepository.Create(car);
    }

    public async Task<GetCarsQueryResult> Handle(GetCarsQuery query)
    {
        IEnumerable<Car> repoResult = repositoryService.CarRepository.GetAll();
        IEnumerable<CarDto> carDtos = repoResult.Select(CarDtoMapper.CarToCarDto);

        return new GetCarsQueryResult(carDtos);
    }
}