using Fahrtenbuch.Domain.Happenings;
using Fahrtenbuch.Domain.Mileages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fahrtenbuch.Infrastructure.Persistence.Happenings;

public class HappeningConfiguration : IEntityTypeConfiguration<Happening>
{
    public virtual void Configure(EntityTypeBuilder<Happening> builder)
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
        builder
            .HasOne<Mileage>()
            .WithMany()
            .HasForeignKey(m => m.MileageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
