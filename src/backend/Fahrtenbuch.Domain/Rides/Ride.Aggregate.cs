using ErrorOr;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Rides;

public class Ride : Aggregate
{
    public required RideId Id { get; init; }
    public required string Description { get; init; }
    public required MileageId StartMileageId { get; init; }
    public MileageId? EndMileageId { get; private set; }

    private Ride()
    {
        // Required for EF Core
    }

    public static ErrorOr<Ride> Create(RideId id, string description, MileageId startMileage)
    {
        return new Ride
        {
            Id = id,
            Description = description,
            StartMileageId = startMileage,
            EndMileageId = null
        };
    }

    internal ErrorOr<Ride> EndRide(MileageId endMileage)
    {
        if (EndMileageId != null)
        {
            return Error.Validation(code: "RideAlreadyEnded", description: "The ride has already been ended.");
        }

        EndMileageId = endMileage;

        return this;
    }
}