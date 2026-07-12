namespace Fahrtenbuch.Application.commands;

public record CreateMileageCommand(Guid CarId, decimal Value, DateTime Date);
