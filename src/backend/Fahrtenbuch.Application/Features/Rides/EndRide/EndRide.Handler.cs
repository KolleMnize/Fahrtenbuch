using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;
namespace Fahrtenbuch.Application.Features.Rides.EndRide;

public class EndRideHandler(IMileageRepository mileageRepository, IRideRepository rideRepository, RideDomainService rideService) : ICommandHandler<EndRideCommand, RideDto>
{
    public async Task<ErrorOr<RideDto>> Handle(EndRideCommand command, CancellationToken ct = default)
    {
        var mileageIdCreateResult = MileageId.Create(command.EndMileageId);
        if (mileageIdCreateResult.IsError)
            return mileageIdCreateResult.Errors;

        var mileageExistsResult = await mileageRepository.Exists(mileageIdCreateResult.Value, ct);
        if (mileageExistsResult.IsError)
            return mileageExistsResult.Errors;

        if (!mileageExistsResult.Value)
            return Error.Validation(code: "Mileage.Validation", description: $"Mileage with id {command.EndMileageId} does not exist.");

        var rideIdCreateResult = RideId.Create(command.RideId);
        if (rideIdCreateResult.IsError)
            return rideIdCreateResult.Errors;

        var rideGetResult = await rideRepository.GetById(rideIdCreateResult.Value, ct);
        if (rideGetResult.IsError)
            return rideGetResult.Errors;

        var ride = rideGetResult.Value;
        if (ride == null)
            return Error.Validation(code: "Ride.Validation", description: $"Ride with id {command.RideId} does not exist.");

        var endMileageIdCreateResult = MileageId.Create(command.EndMileageId);
        if (endMileageIdCreateResult.IsError)
            return endMileageIdCreateResult.Errors;

        var rideEndRideResult = await rideService.EndRide(ride, endMileageIdCreateResult.Value);

        if (rideEndRideResult.IsError)
            return rideEndRideResult.Errors;

        var updateRideResult = await rideRepository.Update(rideEndRideResult.Value, ct);
        if (updateRideResult.IsError)
            return updateRideResult.Errors;

        return RideDtoMapper.RideToRideDto(rideEndRideResult.Value);
    }
}
