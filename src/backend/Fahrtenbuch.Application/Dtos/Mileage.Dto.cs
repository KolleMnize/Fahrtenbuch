namespace Fahrtenbuch.Application.Dtos;

public record MileageDto(Guid MileageId, Guid CarId, decimal Value, DateTime Date);
