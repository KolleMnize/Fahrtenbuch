using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fahrtenbuch.Infrastructure.Persistence.Configurations;

public class HappeningConfiguration : IEntityTypeConfiguration<Happening>
{
    public void Configure(EntityTypeBuilder<Happening> builder)
    {
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => HappeningId.Create(value).Value);
        builder
            .HasIndex(e => e.MileageId);
        builder
            .Property(e => e.MileageId)
            .HasConversion(
                id => id!.Value,
                value => MileageId.Create(value).Value!);
    }
}
