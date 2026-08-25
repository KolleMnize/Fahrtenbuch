using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IRideRepository
{
    Task Create(Ride ride);
    Task Update(Ride ride);
    Task<IEnumerable<Ride>> GetAll();
    Task<Ride?> GetById(RideId rideId);
    Task<bool> Exists(RideId rideId);
}