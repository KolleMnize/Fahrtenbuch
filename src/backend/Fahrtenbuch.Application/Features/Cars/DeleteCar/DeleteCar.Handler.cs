using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Cars;

namespace Fahrtenbuch.Application.Features.Cars.DeleteCar;

public class DeleteCarHandler(
    ICarRepository carRepository,
    ICarDeletionService carDeletionDomainService
    ) : ICommandHandler<DeleteCarCommand, Deleted>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteCarCommand command, CancellationToken ct = default)
    {
        var carIdResult = CarId.Create(command.CarId);
        if (carIdResult.IsError)
            return carIdResult.Errors;

        var deletableResult = await carDeletionDomainService.EnsureDeletable(carIdResult.Value, ct);
        if (deletableResult.IsError)
            return deletableResult.Errors;

        var deleteResult = await carRepository.Delete(carIdResult.Value, ct);
        if (deleteResult.IsError)
            return deleteResult.Errors;

        return Result.Deleted;
    }
}
