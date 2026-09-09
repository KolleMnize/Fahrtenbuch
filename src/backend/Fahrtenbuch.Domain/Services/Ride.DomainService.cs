using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Services;

public class RideDomainService(IMileageRepository mileageRepository)
{
    public async Task<ErrorOr<Ride>> EndRide(Ride ride, MileageId endMileage, CancellationToken cancellationToken = default)
    {
        var startMileageResult = await mileageRepository.GetById(ride.StartMileageId, cancellationToken);
        if (startMileageResult.IsError)
            return startMileageResult.Errors;
        var startMileageEntity = startMileageResult.Value;

        var endMileageEntityResult = await mileageRepository.GetById(endMileage, cancellationToken);
        if (endMileageEntityResult.IsError)
            return endMileageEntityResult.Errors;
        var endMileageEntity = endMileageEntityResult.Value;

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

        return ride.EndRide(endMileage);
    }
}