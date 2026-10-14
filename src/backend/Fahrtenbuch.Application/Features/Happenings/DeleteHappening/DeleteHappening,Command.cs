using ErrorOr;
using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Happenings.DeleteHappening;

public record DeleteHappeningCommand(Guid HappeningId) : ICommand<Deleted>;