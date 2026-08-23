using Microsoft.EntityFrameworkCore;
using Fahrtenbuch.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Infrastructure.Persistence.Configurations;

public class RideConfiguration : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> builder)
    {
        builder
            .HasKey(m => m.Id);
        builder
            .Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => RideId.Create(value).Value);
        builder
            .HasIndex(m => m.StartMileageId);
        builder
            .Property(m => m.StartMileageId)
            .HasConversion(
                id => id.Value,
                value => MileageId.Create(value).Value);
        builder
            .HasIndex(e => e.EndMileageId);
        builder
            .Property(e => e.EndMileageId)
            .HasConversion(
                id => id!.Value,
                value => MileageId.Create(value!).Value);
    }
}