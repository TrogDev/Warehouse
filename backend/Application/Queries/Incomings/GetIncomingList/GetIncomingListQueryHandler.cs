using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Incomings.GetIncomingList;

public class GetIncomingListQueryHandler
    : IRequestHandler<GetIncomingListQuery, IEnumerable<Incoming>>
{
    private readonly IApplicationDbContext context;

    public GetIncomingListQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Incoming>> Handle(
        GetIncomingListQuery request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Incoming> query = context
            .Incomings.Include(e => e.Items)
            .ThenInclude(e => e.Resource)
            .Include(e => e.Items)
            .ThenInclude(e => e.Unit);

        query = ApplyFilter(query, request);

        return await query.ToListAsync(cancellationToken);
    }

    private IQueryable<Incoming> ApplyFilter(
        IQueryable<Incoming> query,
        GetIncomingListQuery request
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
