using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Incoming> Incomings { get; set; }
    public DbSet<Outgoing> Outgoings { get; set; }
    public DbSet<IncomingItem> IncomingItems { get; set; }
    public DbSet<OutgoingItem> OutgoingItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
