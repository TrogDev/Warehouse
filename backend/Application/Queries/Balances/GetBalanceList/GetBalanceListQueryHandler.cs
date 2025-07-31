using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Balances.GetBalanceList;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceListQuery, IEnumerable<Balance>>
{
    private readonly IApplicationDbContext context;

    public GetBalanceQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Balance>> Handle(
        GetBalanceListQuery request,
        CancellationToken cancellationToken
    )
    {
        List<Balance> balances = await ApplyFilter<IncomingItem, long>(
                context.IncomingItems,
                request
            )
            .GroupBy(e => new { e.ResourceId, e.UnitId })
            .Select(
                e =>
                    new Balance
                    {
                        ResourceId = e.Key.ResourceId,
                        UnitId = e.Key.UnitId,
                        Quantity = e.Sum(i => i.Quantity)
                    }
            )
            .ToListAsync(cancellationToken);

        List<Balance> spentList = await ApplyFilter<OutgoingItem, long>(
                context.OutgoingItems,
                request
            )
            .Include(e => e.Outgoing)
            .Where(e => e.Outgoing.IsSigned)
            .GroupBy(e => new { e.ResourceId, e.UnitId })
            .Select(
                e =>
                    new Balance
                    {
                        ResourceId = e.Key.ResourceId,
                        UnitId = e.Key.UnitId,
                        Quantity = e.Sum(i => i.Quantity)
                    }
            )
            .ToListAsync(cancellationToken);

        foreach (Balance balance in spentList)
        {
            Balance? existingBalance = balances.FirstOrDefault(
                b => b.ResourceId == balance.ResourceId && b.UnitId == balance.UnitId
            );

            if (existingBalance != null)
            {
                existingBalance.Quantity -= balance.Quantity;
            }
        }

        return balances;
    }

    private IQueryable<TEntity> ApplyFilter<TEntity, TKey>(
        IQueryable<TEntity> query,
        GetBalanceListQuery request
    )
        where TEntity : ResourceUnitEntity<TKey>
    {
        if (request.Resources != null)
        {
            query = query.Where(e => request.Resources.Contains(e.ResourceId));
        }
        if (request.Units != null)
        {
            query = query.Where(e => request.Units.Contains(e.UnitId));
        }

        return query;
    }
}
