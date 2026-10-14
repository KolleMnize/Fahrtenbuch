using ErrorOr;
using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Mileages.DeleteMilegae;

public record DeleteMileageCommand(Guid MileageId) : ICommand<Deleted>;
