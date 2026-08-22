using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fahrtenbuch.Infrastructure.Persistence.Configurations;

public class MileageConfiguration : IEntityTypeConfiguration<Mileage>
{
    public void Configure(EntityTypeBuilder<Mileage> builder)
    {
        builder
        .HasKey(m => m.Id);
        builder
        .Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => MileageId.Create(value).Value);
        builder
        .HasIndex(m => m.CarId);
        builder
        .Property(m => m.CarId)
                .HasConversion(
                    id => id.Value,
                    value => CarId.Create(value).Value);
    }
}