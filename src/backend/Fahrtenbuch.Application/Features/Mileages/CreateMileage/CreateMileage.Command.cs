using Fahrtenbuch.Application.Features.Mileages;
using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Mileages.CreateMileage;

public record CreateMileageCommand(Guid CarId, decimal Value, DateTime Date) : ICommand<MileageDto>;
