using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Cars.CreateCar;

public record CreateCarCommand(string Name) : ICommand<CarDto>;
