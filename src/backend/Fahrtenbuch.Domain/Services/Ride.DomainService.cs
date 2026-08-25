using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Services;

public class RideDomainService(IMileageRepository mileageRepository)
{
    public async Task<ErrorOr<Ride>> EndRide(Ride ride, MileageId endMileage)
    {
        Mileage? startMileageEntity = await mileageRepository.GetById(ride.StartMileageId);
        Mileage? endMileageEntity = await mileageRepository.GetById(endMileage);

        if (startMileageEntity == null)
        {
            return Error.Validation(code: "StartMileageNotFound", description: $"Start mileage with ID {ride.StartMileageId} does not exist.");
        }
        if (endMileageEntity == null)
        {
            return Error.Validation(code: "EndMileageNotFound", description: $"End mileage with ID {endMileage} does not exist.");
        }
        if (startMileageEntity.CarId != endMileageEntity.CarId)
        {
            return Error.Validation(code: "MismatchedCarIds", description: "The start and end mileage must belong to the same car.");
        }
        if (endMileageEntity.Value < startMileageEntity.Value)
        {
            return Error.Validation(code: "InvalidMileageValue", description: "The end mileage value cannot be lower than the start mileage value.");
        }

        var rideEndRideResult = ride.EndRide(endMileage);

        if (rideEndRideResult.IsError)
        {
            return rideEndRideResult.Errors;
        }

        return rideEndRideResult.Value;
    }
}