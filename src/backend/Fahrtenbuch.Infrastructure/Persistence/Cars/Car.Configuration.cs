using Fahrtenbuch.Domain.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fahrtenbuch.Infrastructure.Persistence.Cars;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public virtual void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(car => car.Id);

        builder.Property(car => car.Id)
            .HasConversion(
                id => id.Value,
                value => CarId.Create(value).Value);
    }
}

