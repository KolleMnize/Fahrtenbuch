using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using ErrorOr;
namespace Fahrtenbuch.Domain.Services;

public class MileageDomainService(IMileageRepository mileageRepository)
{
    public async Task<ErrorOr<Mileage>> CreateMileage(MileageId id, CarId carId, decimal value, DateTime date, CancellationToken ct = default)
    {
        var previousMileageResult = await mileageRepository.GetPreviousMileageFromDate(carId, date, ct);
        if (previousMileageResult.IsError)
            return previousMileageResult.Errors;

        Mileage? previousMileage = previousMileageResult.Value;

        var followingMileageResult = await mileageRepository.GetFollowingMileageFromDate(carId, date, ct);
        if (followingMileageResult.IsError)
            return followingMileageResult.Errors;

        Mileage? followingMileage = followingMileageResult.Value;

        if (previousMileage != null && value < previousMileage.Value)
            return Error.Validation(code: "InvalidMileageValue", description: "The mileage value cannot be lower than the previous mileage.");

        if (followingMileage != null && value > followingMileage.Value)
            return Error.Validation(code: "InvalidMileageValue", description: "The mileage value cannot be higher than the following mileage.");

        var mileageCreateResult = Mileage.Create(id, carId, value, date);

        if (mileageCreateResult.IsError)
            return mileageCreateResult.Errors;

        return mileageCreateResult.Value;
    }
}