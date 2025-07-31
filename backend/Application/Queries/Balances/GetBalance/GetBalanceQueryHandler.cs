using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Queries.Balances.GetBalance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, int>
{
    private readonly IApplicationDbContext context;

    public GetBalanceQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        int recieved = await context
            .IncomingItems.Where(
                e => e.ResourceId == request.ResourceId && e.UnitId == request.UnitId
            )
            .SumAsync(e => e.Quantity, cancellationToken);

        int spent = await context
            .OutgoingItems.Include(e => e.Outgoing)
            .Where(
                e =>
                    e.ResourceId == request.ResourceId
                    && e.UnitId == request.UnitId
                    && e.Outgoing.IsSigned
            )
            .SumAsync(e => e.Quantity, cancellationToken);

        return recieved - spent;
    }
}
