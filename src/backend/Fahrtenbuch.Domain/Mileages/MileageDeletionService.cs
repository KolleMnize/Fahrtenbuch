using ErrorOr;
using Fahrtenbuch.Domain.Happenings;
using Fahrtenbuch.Domain.Rides;
using Fahrtenbuch.Domain.SharedKernel.Deletion;

namespace Fahrtenbuch.Domain.Mileages;

internal class MileageDeletionService(
    IMileageRepository mileageRepository,
    IRideRepository rideRepository,
    IHappeningRepository happeningRepository
    ) : DeletionDomainServiceBase<MileageId>, IMileageDeletionService
{
    protected internal override async Task<ErrorOr<IReadOnlyList<DeletionBlockingReference>>> GetBlockingReasons(MileageId aggregateId, CancellationToken ct = default)
    {
        var blockingReasons = new List<DeletionBlockingReference>();
        var mileageResult = await mileageRepository.GetById(aggregateId, ct);
        if (mileageResult.IsError)
            return mileageResult.Errors;

        var mileage = mileageResult.Value;

        var ridesExistsResult = await rideRepository.ExistsForMileage(aggregateId, ct);
        if (ridesExistsResult.IsError)
            return ridesExistsResult.Errors;

        if (ridesExistsResult.Value)
        {
            var ridesByMileageResult = await rideRepository.GetAllByMileage(aggregateId, ct);
            if (ridesByMileageResult.IsError)
                return ridesByMileageResult.Errors;

            var ridesByMileage = ridesByMileageResult.Value;
            foreach (var ride in ridesByMileage)
                blockingReasons.Add(new DeletionBlockingReference(mileage, ride));
        }


        var happeningsExistsResult = await happeningRepository.ExistsForMileage(aggregateId, ct);
        if (happeningsExistsResult.IsError)
            return happeningsExistsResult.Errors;

        if (happeningsExistsResult.Value)
        {
            var happeningsByMileageResult = await happeningRepository.GetAllByMileage(aggregateId, ct);
            if (happeningsByMileageResult.IsError)
                return happeningsByMileageResult.Errors;

            var happeningsByMileage = happeningsByMileageResult.Value;
            foreach (var happening in happeningsByMileage)
                blockingReasons.Add(new DeletionBlockingReference(mileage, happening));
        }
        return blockingReasons;
    }
}