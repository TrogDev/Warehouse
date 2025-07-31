using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data.Configurations;

public class IncomingItemConfiguration : IEntityTypeConfiguration<IncomingItem>
{
    public void Configure(EntityTypeBuilder<IncomingItem> builder)
    {
        builder
            .HasOne(e => e.Incoming)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.IncomingId)
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
