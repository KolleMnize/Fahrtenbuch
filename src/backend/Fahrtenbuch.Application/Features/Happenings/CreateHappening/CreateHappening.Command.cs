using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Happenings.CreateHappening;

public record CreateHappeningCommand(string Description, Guid MileageId) : ICommand<HappeningDto>;
