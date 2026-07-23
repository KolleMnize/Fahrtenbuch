using ErrorOr;
using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Application.mapper;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.services;

namespace Fahrtenbuch.Application.services;

public class MileageManagementService(RepositoryService repositoryService)
{
    public async Task<ErrorOr<Success>> Handle(CreateMileageCommand command)
    {
        var carExists = repositoryService.CarRepository.Exists(CarId.Create(command.CarId).Value);
        if (!carExists)
            return Error.Validation(code: "CarNotFound", description: $"Car with id {command.CarId} does not exist.");

        var mileage = Mileage.Create(
            MileageId.Create(Guid.NewGuid()).Value,
            CarId.Create(command.CarId).Value,
            command.Value,
            command.Date);
        repositoryService.MileageRepository.Create(mileage);

        return Result.Success;
    }

    public async Task<GetMileagesQueryResult> Handle(GetMileagesQuery query)
    {
        IEnumerable<Mileage> repoResult = repositoryService.MileageRepository.GetAll();
        IEnumerable<MileageDto> mileageDtos = repoResult.Select(MileageDtoMapper.MileageToMileageDto);

        return new GetMileagesQueryResult(mileageDtos);
    }
}