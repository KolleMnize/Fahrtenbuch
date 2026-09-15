using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Happenings;

namespace Fahrtenbuch.Application.Features.Happenings.DeleteHappening;

public class DeleteHappeningHandler(
    IHappeningDeletionService happeningDeletionService,
    IHappeningRepository happeningRepository
) : ICommandHandler<DeleteHappeningCommand, Deleted>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteHappeningCommand command, CancellationToken ct = default)
    {
        var happeningIdResult = HappeningId.Create(command.HappeningId);
        if (happeningIdResult.IsError)
            return happeningIdResult.Errors;

        var deletableResult = await happeningDeletionService.EnsureDeletable(happeningIdResult.Value, ct);
        if (deletableResult.IsError)
            return deletableResult.Errors;

        var deleteResult = await happeningRepository.Delete(happeningIdResult.Value, ct);
        if (deleteResult.IsError)
            return deleteResult.Errors;

        return Result.Deleted;

    }

}