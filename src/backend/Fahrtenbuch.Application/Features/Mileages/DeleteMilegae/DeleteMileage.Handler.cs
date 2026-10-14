using Fahrtenbuch.Application.Interfaces;
using ErrorOr;
using Fahrtenbuch.Domain.Mileages;

namespace Fahrtenbuch.Application.Features.Mileages.DeleteMilegae;

public class DeleteMileageHandler(
    IMileageDeletionService mileageDeletionDomainService,
    IMileageRepository mileageRepository
    ) : ICommandHandler<DeleteMileageCommand, Deleted>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteMileageCommand command, CancellationToken ct = default)
    {
        var mileageIdResult = MileageId.Create(command.MileageId);
        if (mileageIdResult.IsError)
            return mileageIdResult.Errors;

        var deletableResult = await mileageDeletionDomainService.EnsureDeletable(mileageIdResult.Value, ct);
        if (deletableResult.IsError)
            return deletableResult.Errors;

        var deleteResult = await mileageRepository.Delete(mileageIdResult.Value, ct);
        if (deleteResult.IsError)
            return deleteResult.Errors;

        return Result.Deleted;
    }
}