namespace Fahrtenbuch.Application.Commands;

public record CreateRideCommand(Guid RideId, string Description, Guid StartMileageId);
