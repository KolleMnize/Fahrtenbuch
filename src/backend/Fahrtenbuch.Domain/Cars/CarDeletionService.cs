using ErrorOr;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.SharedKernel.Deletion;

namespace Fahrtenbuch.Domain.Cars;

internal class CarDeletionService(IMileageRepository mileageRepository, ICarRepository carRepository) : DeletionDomainServiceBase<CarId>, ICarDeletionService
{
    protected internal override async Task<ErrorOr<IReadOnlyList<DeletionBlockingReference>>> GetBlockingReasons(CarId aggregateId, CancellationToken ct = default)
    {
        var blockingReasons = new List<DeletionBlockingReference>();

        var carResult = await carRepository.GetById(aggregateId, ct);
        if (carResult.IsError)
            return carResult.Errors;

        var car = carResult.Value;

        var mileagesExistResult = await mileageRepository.ExistsForCar(aggregateId, ct);
        if (mileagesExistResult.IsError)
            return mileagesExistResult.Errors;

        if (mileagesExistResult.Value == false)
            return blockingReasons;

        var mileagesByCarResult = await mileageRepository.GetAllByCar(aggregateId, ct);
        if (mileagesByCarResult.IsError)
            return mileagesByCarResult.Errors;

        var mileagesByCar = mileagesByCarResult.Value;

        foreach (var mileage in mileagesByCar)
            blockingReasons.Add(
                new DeletionBlockingReference(car, mileage));

        return blockingReasons;
    }
}
