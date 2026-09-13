using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.Rides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Fahrtenbuch.Infrastructure.Persistence.Rides;

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
        builder
            .HasOne<Mileage>()
            .WithMany()
            .HasForeignKey(m => m.StartMileageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Mileage>()
            .WithMany()
            .HasForeignKey(m => m.EndMileageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}