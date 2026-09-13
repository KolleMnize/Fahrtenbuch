using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Rides;

namespace Fahrtenbuch.Application.Features.Rides.DeleteRide;

public class DeleteRideHandler(IRideRepository rideRepository, IRideDeletionService rideDeletionDomainService) : ICommandHandler<DeleteRideCommand, Deleted>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteRideCommand command, CancellationToken ct = default)
    {
        var rideIdResult = RideId.Create(command.RideId);
        if (rideIdResult.IsError)
            return rideIdResult.Errors;

        var deletableResult = await rideDeletionDomainService.EnsureDeletable(rideIdResult.Value, ct);
        if (deletableResult.IsError)
            return deletableResult.Errors;

        var deleteResult = await rideRepository.Delete(rideIdResult.Value, ct);
        if (deleteResult.IsError)
            return deleteResult.Errors;

        return Result.Deleted;
    }
}
