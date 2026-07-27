using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface ICarRepository
{
    bool Exists(CarId carId);
    void Create(Car car);
    IEnumerable<Car> GetAll();

}