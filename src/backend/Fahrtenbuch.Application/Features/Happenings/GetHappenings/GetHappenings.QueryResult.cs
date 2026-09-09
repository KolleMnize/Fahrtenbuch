namespace Fahrtenbuch.Application.Features.Happenings.GetHappenings;

public record GetHappeningsQueryResult(IReadOnlyList<HappeningDto> Result);
