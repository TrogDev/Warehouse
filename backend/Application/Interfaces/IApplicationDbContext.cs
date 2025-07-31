namespace Warehouse.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;

public interface IApplicationDbContext
{
    DbSet<Resource> Resources { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<Unit> Units { get; set; }
    DbSet<Incoming> Incomings { get; set; }
    DbSet<Outgoing> Outgoings { get; set; }
    DbSet<IncomingItem> IncomingItems { get; set; }
    DbSet<OutgoingItem> OutgoingItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
