using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IMileageRepository
{
    Task Create(Mileage mileage);
    Task<IEnumerable<Mileage>> GetAll();
    Task<Mileage?> GetById(MileageId mileageId);
    Task<Mileage?> GetFollowingMileageFromDate(CarId carId, DateTime date);
    Task<Mileage?> GetPreviousMileageFromDate(CarId carId, DateTime date);
    Task<bool> Exists(MileageId mileageId);
}
