using ErrorOr;
using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Application.mapper;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.interfaces.repositories;
using Fahrtenbuch.Domain.services;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Application.services;

public class MileageManagementService(ICarRepository carRepository, IMileageRepository mileageRepository, MileageDomainService mileageService)
{
    public async Task<ErrorOr<Success>> Handle(CreateMileageCommand command)
    {
        var carExists = carRepository.Exists(CarId.Create(command.CarId).Value);
        if (!carExists)
            return Error.Validation(code: "CarNotFound", description: $"Car with id {command.CarId} does not exist.");

        var mileage = mileageService.CreateMileage(
            MileageId.Create(Guid.NewGuid()).Value,
            CarId.Create(command.CarId).Value,
            command.Value,
            command.Date);
        mileageRepository.Create(mileage);

        return Result.Success;
    }

    public async Task<GetMileagesQueryResult> Handle(GetMileagesQuery query)
    {
        IEnumerable<Mileage> repoResult = mileageRepository.GetAll();
        IEnumerable<MileageDto> mileageDtos = repoResult.Select(MileageDtoMapper.MileageToMileageDto);

        return new GetMileagesQueryResult(mileageDtos);
    }
}