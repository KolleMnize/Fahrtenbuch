using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface ICarRepository
{
    Task<bool> Exists(CarId carId);
    Task Create(Car car);
    Task<IEnumerable<Car>> GetAll();
}