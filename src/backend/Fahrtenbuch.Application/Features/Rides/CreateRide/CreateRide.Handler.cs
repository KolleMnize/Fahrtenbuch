using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Features.Rides.CreateRide;

public class CreateRideHandler(IMileageRepository mileageRepository, IRideRepository rideRepository) : ICommandHandler<CreateRideCommand, RideDto>
{
    public async Task<ErrorOr<RideDto>> Handle(CreateRideCommand command, CancellationToken ct = default)
    {
        var mileageIdCreateResult = MileageId.Create(command.StartMileageId);
        if (mileageIdCreateResult.IsError)
            return mileageIdCreateResult.Errors;

        var mileageExistsResult = await mileageRepository.Exists(mileageIdCreateResult.Value, ct);
        if (mileageExistsResult.IsError)
            return mileageExistsResult.Errors;
        if (!mileageExistsResult.Value)
            return Error.Validation(code: "Mileage.Validation", description: $"Mileage with id {command.StartMileageId} does not exist.");

        var rideCreateResult = Ride.Create(
            RideId.Create(Guid.NewGuid()).Value,
            command.Description,
            mileageIdCreateResult.Value);
        if (rideCreateResult.IsError)
            return rideCreateResult.Errors;

        var dbResult = await rideRepository.Create(rideCreateResult.Value, ct);
        if (dbResult.IsError)
            return dbResult.Errors;

        return RideDtoMapper.RideToRideDto(rideCreateResult.Value);
    }

}