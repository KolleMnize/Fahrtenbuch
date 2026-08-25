using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using ErrorOr;
namespace Fahrtenbuch.Domain.Services;

public class MileageDomainService(IMileageRepository mileageRepository)
{
    public async Task<ErrorOr<Mileage>> CreateMileage(MileageId id, CarId carId, decimal value, DateTime date)
    {
        Mileage? previousMileage = await mileageRepository.GetPreviousMileageFromDate(carId, date);
        Mileage? followingMileage = await mileageRepository.GetFollowingMileageFromDate(carId, date);

        if (previousMileage != null && value < previousMileage.Value)
        {
            return Error.Validation(code: "InvalidMileageValue", description: "The mileage value cannot be lower than the previous mileage.");
        }
        if (followingMileage != null && value > followingMileage.Value)
        {
            return Error.Validation(code: "InvalidMileageValue", description: "The mileage value cannot be higher than the following mileage.");
        }

        var mileageCreateResult = Mileage.Create(id, carId, value, date);

        if (mileageCreateResult.IsError)
        {
            return mileageCreateResult.Errors;
        }

        return mileageCreateResult.Value;
    }
}