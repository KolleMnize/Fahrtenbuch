using ErrorOr;
using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Rides.DeleteRide;

public record DeleteRideCommand(Guid RideId) : ICommand<Deleted>;