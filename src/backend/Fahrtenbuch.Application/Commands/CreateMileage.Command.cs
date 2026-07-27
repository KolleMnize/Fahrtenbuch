namespace Fahrtenbuch.Application.Commands;

public record CreateMileageCommand(Guid CarId, decimal Value, DateTime Date);
