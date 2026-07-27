using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;

namespace Fahrtenbuch.Domain.interfaces.repositories;

public interface ICarRepository
{
    bool Exists(CarId carId);
    void Create(Car car);
    IEnumerable<Car> GetAll();

}