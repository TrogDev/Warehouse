using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data.Configurations;

public class OutgoingItemConfiguration : IEntityTypeConfiguration<OutgoingItem>
{
    public void Configure(EntityTypeBuilder<OutgoingItem> builder)
    {
        builder
            .HasOne(e => e.Outgoing)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.OutgoingId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(e => e.Resource)
            .WithMany()
            .HasForeignKey(e => e.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(e => e.Unit)
            .WithMany()
            .HasForeignKey(e => e.UnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
