using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fahrtenbuch.Infrastructure.Persistence.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public virtual void Configure(EntityTypeBuilder<Car> builder)
    {
        builder
        .HasKey(c => c.Id);
        builder
        .Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => CarId.Create(value).Value);
    }
}

