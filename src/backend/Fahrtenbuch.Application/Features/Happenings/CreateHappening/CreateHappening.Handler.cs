using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Features.Happenings.CreateHappening;

public class CreateHappeningHandler(
    IHappeningRepository happeningRepository,
    IMileageRepository mileageRepository) : ICommandHandler<CreateHappeningCommand, HappeningDto>
{
    public async Task<ErrorOr<HappeningDto>> Handle(CreateHappeningCommand command, CancellationToken ct = default)
    {
        var mileageIdCreateResult = MileageId.Create(command.MileageId);
        if (mileageIdCreateResult.IsError)
            return mileageIdCreateResult.Errors;


        var mileageExistsResult = await mileageRepository.Exists(mileageIdCreateResult.Value, ct);
        if (mileageExistsResult.IsError)
            return mileageExistsResult.Errors;
        if (!mileageExistsResult.Value)
            return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.MileageId} does not exist.");


        var happeningCreateResult = Happening.Create(
            HappeningId.Create(Guid.NewGuid()).Value,
            command.Description,
            MileageId.Create(command.MileageId).Value);
        if (happeningCreateResult.IsError)
            return happeningCreateResult.Errors;


        var dbResult = await happeningRepository.Create(happeningCreateResult.Value, ct);
        if (dbResult.IsError)
            return dbResult.Errors;


        return HappeningDtoMapper.HappeningToHappeningDto(happeningCreateResult.Value);
    }
}