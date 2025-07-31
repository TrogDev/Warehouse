using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Queries.Resources.GetResourceList;

public class GetResourceQueryListHandler
    : IRequestHandler<GetResourceListQuery, IEnumerable<Resource>>
{
    private readonly IApplicationDbContext context;

    public GetResourceQueryListHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Resource>> Handle(
        GetResourceListQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context.Resources.ToListAsync(cancellationToken);
    }
}
