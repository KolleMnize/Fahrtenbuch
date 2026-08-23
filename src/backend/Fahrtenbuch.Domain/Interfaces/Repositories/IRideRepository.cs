using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IRideRepository
{
    void Create(Ride ride);
    void Update(Ride ride);
    IEnumerable<Ride> GetAll();
    Ride? GetById(RideId rideId);
    bool Exists(RideId rideId);
}