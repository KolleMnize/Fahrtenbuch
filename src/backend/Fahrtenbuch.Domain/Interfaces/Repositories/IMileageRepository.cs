using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IMileageRepository
{
    void Create(Mileage mileage);
    IEnumerable<Mileage> GetAll();
    Mileage? GetById(MileageId mileageId);
    Mileage? GetFollowingMileageFromDate(CarId carId, DateTime date);
    Mileage? GetPreviousMileageFromDate(CarId carId, DateTime date);
    bool Exists(MileageId mileageId);
}
