using Fahrtenbuch.Application.Dtos;

namespace Fahrtenbuch.Application.Querys;

public record GetCarsQueryResult(IEnumerable<CarDto> Result);

