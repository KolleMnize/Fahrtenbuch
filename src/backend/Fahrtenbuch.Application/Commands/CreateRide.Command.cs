namespace Fahrtenbuch.Application.Commands;

public record CreateRideCommand(string Description, Guid StartMileageId);
