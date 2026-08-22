namespace Fahrtenbuch.Application.Commands;

public record CreateHappeningCommand(string Description, Guid MileageId);
