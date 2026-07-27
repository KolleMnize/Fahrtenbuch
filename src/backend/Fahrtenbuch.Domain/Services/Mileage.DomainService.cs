using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Domain.Interfaces.Repositories;
namespace Fahrtenbuch.Domain.Services;

public class MileageDomainService(IMileageRepository mileageRepository)
{
    public Mileage CreateMileage(MileageId id, CarId carId, decimal value, DateTime date)
    {
        Mileage? previousMileage = mileageRepository.GetPreviousMileageFromDate(carId, date);
        Mileage? followingMileage = mileageRepository.GetFollowingMileageFromDate(carId, date);

        if (previousMileage != null && value < previousMileage.Value)
        {
            throw new InvalidOperationException("The mileage value cannot be lower than the previous mileage.");
        }
        if (followingMileage != null && value > followingMileage.Value)
        {
            throw new InvalidOperationException("The mileage value cannot be higher than the following mileage.");
        }

        var mileage = Mileage.Create(id, carId, value, date);
        return mileage;
    }
}