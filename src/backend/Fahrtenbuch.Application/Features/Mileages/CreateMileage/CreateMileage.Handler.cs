
using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Features.Mileages.CreateMileage;

public class CreateMileageHandler(
    MileageDomainService mileageDomainService,
    ICarRepository carRepository,
    IMileageRepository mileageRepository) : ICommandHandler<CreateMileageCommand, MileageDto>
{
    public async Task<ErrorOr<MileageDto>> Handle(CreateMileageCommand command, CancellationToken ct = default)
    {
        var carIdCreateResult = CarId.Create(command.CarId);
        if (carIdCreateResult.IsError)
            return carIdCreateResult.Errors;

        var carExistingResult = await carRepository.Exists(carIdCreateResult.Value, ct);
        if (carExistingResult.IsError)
            return carExistingResult.Errors;

        if (!carExistingResult.Value)
            return Error.Validation(code: "CarNotFound", description: $"Car with id {command.CarId} does not exist.");

        var mileageCreateResult = await mileageDomainService.CreateMileage(
            MileageId.Create(Guid.NewGuid()).Value,
            CarId.Create(command.CarId).Value,
            command.Value,
            command.Date,
            ct);

        if (mileageCreateResult.IsError)
            return mileageCreateResult.Errors;

        var createMileageResult = await mileageRepository.Create(mileageCreateResult.Value, ct);

        if (createMileageResult.IsError)
            return createMileageResult.Errors;

        return MileageDtoMapper.MileageToMileageDto(mileageCreateResult.Value);
    }

}