using Fahrtenbuch.Domain.Happenings;

namespace Fahrtenbuch.Application.Features.Happenings;

internal static class HappeningDtoMapper
{
    public static HappeningDto HappeningToHappeningDto(Happening happening)
    {
        return new HappeningDto(
            happening.Id.Value,
            happening.Description,
            happening.MileageId != null ? happening.MileageId.Value : null);
    }
}
