using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Data;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext context;

    public ApplicationDbContextInitializer(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        context.Database.Migrate();
        FillEntities();
    }

    private void FillEntities()
    {
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(
                [
                    new Client() { Address = "TestClientAddress1", Name = "TestClientName1" },
                    new Client() { Address = "TestClientAddress2", Name = "TestClientName2" }
                ]
            );
        }
        if (!context.Resources.Any())
        {
            context.Resources.AddRange(
                [
                    new Resource() { Name = "TestResourceName1" },
                    new Resource() { Name = "TestResourceName2" }
                ]
            );
        }
        if (!context.Units.Any())
        {
            context.Units.AddRange(
                [new Unit() { Name = "TestUnitName1" }, new Unit() { Name = "TestUnitName2" }]
            );
        }

        context.SaveChanges();
    }
}
