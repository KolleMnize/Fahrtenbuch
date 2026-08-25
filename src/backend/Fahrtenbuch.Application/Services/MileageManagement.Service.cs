using ErrorOr;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Application.Mapper;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Services;

public class MileageManagementService(ICarRepository carRepository, IMileageRepository mileageRepository, MileageDomainService mileageService)
{
    public async Task<ErrorOr<Mileage>> Handle(CreateMileageCommand command)
    {
        bool carExisting = await carRepository.Exists(CarId.Create(command.CarId).Value);

        if (!carExisting)
            return Error.Validation(code: "CarNotFound", description: $"Car with id {command.CarId} does not exist.");

        var mileageCreateResult = await mileageService.CreateMileage(
            MileageId.Create(Guid.NewGuid()).Value,
            CarId.Create(command.CarId).Value,
            command.Value,
            command.Date);

        if (mileageCreateResult.IsError)
        {
            return mileageCreateResult.Errors;
        }

        await mileageRepository.Create(mileageCreateResult.Value);

        return mileageCreateResult.Value;
    }

    public async Task<GetMileagesQueryResult> Handle(GetMileagesQuery query)
    {
        IEnumerable<Mileage> repoResult = await mileageRepository.GetAll();
        IEnumerable<MileageDto> mileageDtos = repoResult.Select(MileageDtoMapper.MileageToMileageDto);

        return new GetMileagesQueryResult(mileageDtos);
    }
}