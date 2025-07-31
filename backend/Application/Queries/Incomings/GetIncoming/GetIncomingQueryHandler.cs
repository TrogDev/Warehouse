using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Incomings.GetIncoming;

public class GetIncomingQueryHandler : IRequestHandler<GetIncomingQuery, Incoming>
{
    private readonly IApplicationDbContext context;

    public GetIncomingQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Incoming> Handle(
        GetIncomingQuery request,
        CancellationToken cancellationToken
    )
    {
        Incoming? incoming = await context
            .Incomings.Include(e => e.Items)
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
