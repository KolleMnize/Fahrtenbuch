using ErrorOr;
using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Cars.DeleteCar;

public record DeleteCarCommand(Guid CarId) : ICommand<Deleted>;