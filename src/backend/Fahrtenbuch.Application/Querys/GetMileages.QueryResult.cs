using Fahrtenbuch.Application.Dtos;

namespace Fahrtenbuch.Application.Querys;

public record GetMileagesQueryResult(IEnumerable<MileageDto> Mileages);
