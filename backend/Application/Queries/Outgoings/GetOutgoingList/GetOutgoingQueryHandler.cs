using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Outgoings.GetOutgoingList;

public class GetOutgoingQueryListHandler
    : IRequestHandler<GetOutgoingListQuery, IEnumerable<Outgoing>>
{
    private readonly IApplicationDbContext context;

    public GetOutgoingQueryListHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Outgoing>> Handle(
        GetOutgoingListQuery request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Outgoing> query = context
            .Outgoings.Include(e => e.Client)
            .Include(e => e.Items)
            .ThenInclude(e => e.Resource)
            .Include(e => e.Items)
            .ThenInclude(e => e.Unit);

        query = ApplyFilter(query, request);

        return await query.ToListAsync(cancellationToken);
    }

    private IQueryable<Outgoing> ApplyFilter(
        IQueryable<Outgoing> query,
        GetOutgoingListQuery request
    )
    {
        if (request.DateFrom != null)
        {
            query = query.Where(e => e.Date >= request.DateFrom);
        }
        if (request.DateTo != null)
        {
            query = query.Where(e => e.Date <= request.DateTo);
        }
        if (request.Numbers != null)
        {
            query = query.Where(e => request.Numbers.Contains(e.Number));
        }
        if (request.Clients != null)
        {
            query = query.Where(e => request.Clients.Contains(e.ClientId));
        }
        if (request.Resources != null)
        {
            query = query.Where(e => e.Items.Any(i => request.Resources.Contains(i.ResourceId)));
        }
        if (request.Units != null)
        {
            query = query.Where(e => e.Items.Any(i => request.Units.Contains(i.UnitId)));
        }

        return query;
    }
}
