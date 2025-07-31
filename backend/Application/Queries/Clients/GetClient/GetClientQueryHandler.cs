using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Clients.GetClient;

public class GetClientQueryHandler : IRequestHandler<GetClientQuery, Client>
{
    private readonly IApplicationDbContext context;

    public GetClientQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Client> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        Client? client = await context.Clients.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (client == null)
        {
            throw new EntityNotFoundException();
        }

        return client;
    }
}
