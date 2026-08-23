using Fahrtenbuch.Domain.SharedKernel;
using Fahrtenbuch.Domain.ValueObjects;
namespace Fahrtenbuch.Domain.Aggregates;

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

    public static Ride Create(RideId id, string description, MileageId startMileage)
    {
        return new Ride
        {
            Id = id,
            Description = description,
            StartMileageId = startMileage,
            EndMileageId = null
        };
    }

    internal void EndRide(MileageId endMileage)
    {
        if (EndMileageId != null)
        {
            throw new InvalidOperationException("Ride has already ended.");
        }

        EndMileageId = endMileage;
    }
}