using ErrorOr;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.SharedKernel;

namespace Fahrtenbuch.Domain.Happenings;

public class Happening : Aggregate<HappeningId>
{
    public string Description { get; private set; } = string.Empty;
    public MileageId? MileageId { get; private set; }

    private Happening()
    {
        // Required by EF
    }

    private Happening(HappeningId id, string description, MileageId? mileageId)
    {
        Id = id;
        Description = description;
        MileageId = mileageId;
    }

    public static ErrorOr<Happening> Create(HappeningId id, string description, MileageId? mileageId = null)
    {
        return new Happening(id, description, mileageId) { Id = id };
    }

    public ErrorOr<Happening> UpdateDescription(string description)
    {
        Description = description;
        return this;
    }
}