using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Queries.Clients.GetClientList;

public class GetClientQueryListHandler : IRequestHandler<GetClientListQuery, IEnumerable<Client>>
{
    private readonly IApplicationDbContext context;

    public GetClientQueryListHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Client>> Handle(
        GetClientListQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context.Clients.ToListAsync(cancellationToken);
    }
}
