using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Outgoings.GetOutgoing;

public class GetOutgoingQueryHandler : IRequestHandler<GetOutgoingQuery, Outgoing>
{
    private readonly IApplicationDbContext context;

    public GetOutgoingQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Outgoing> Handle(
        GetOutgoingQuery request,
        CancellationToken cancellationToken
    )
    {
        Outgoing? incoming = await context
            .Outgoings.Include(e => e.Client)
            .Include(e => e.Items)
            .ThenInclude(e => e.Resource)
            .Include(e => e.Items)
            .ThenInclude(e => e.Unit)
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        if (incoming == null)
        {
            throw new EntityNotFoundException();
        }

        return incoming;
    }
}
