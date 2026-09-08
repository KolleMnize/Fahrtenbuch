namespace Fahrtenbuch.Application.Features.Mileages.GetMileages;

public record GetMileagesQueryResult(IReadOnlyList<MileageDto> Mileages);
