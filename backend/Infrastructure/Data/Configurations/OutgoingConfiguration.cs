using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data.Configurations;

public class OutgoingConfiguration : IEntityTypeConfiguration<Outgoing>
{
    public void Configure(EntityTypeBuilder<Outgoing> builder)
    {
        builder
            .HasOne(e => e.Client)
            .WithMany()
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
