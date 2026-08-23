using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Services;

public class RideDomainService(IMileageRepository mileageRepository)
{
    public Ride EndRide(Ride ride, MileageId endMileage)
    {
        Mileage? startMileageEntity = mileageRepository.GetById(ride.StartMileageId);
        Mileage? endMileageEntity = mileageRepository.GetById(endMileage);

        if (startMileageEntity == null)
        {
            throw new InvalidOperationException($"Start mileage with ID {ride.StartMileageId} does not exist.");
        }
        if (endMileageEntity == null)
        {
            throw new InvalidOperationException($"End mileage with ID {endMileage} does not exist.");
        }
        if (startMileageEntity.CarId != endMileageEntity.CarId)
        {
            throw new InvalidOperationException("Start and end mileage must belong to the same car.");
        }
        if (endMileageEntity.Value < startMileageEntity.Value)
        {
            throw new InvalidOperationException("End mileage cannot be less than start mileage.");
        }

        ride.EndRide(endMileage);
        return ride;
    }
}