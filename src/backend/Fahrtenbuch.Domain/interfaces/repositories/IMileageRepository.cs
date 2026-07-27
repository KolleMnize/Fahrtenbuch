using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
namespace Fahrtenbuch.Domain.interfaces.repositories;

public interface IMileageRepository
{
    void Create(Mileage mileage);
    IEnumerable<Mileage> GetAll();
    Mileage? GetFollowingMileageFromDate(CarId carId, DateTime date);
    Mileage? GetPreviousMileageFromDate(CarId carId, DateTime date);
}
