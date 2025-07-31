using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Queries.Units.GetUnitList;

public class GetUnitQueryListHandler
    : IRequestHandler<GetUnitListQuery, IEnumerable<Domain.Entities.Unit>>
{
    private readonly IApplicationDbContext context;

    public GetUnitQueryListHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Unit>> Handle(
        GetUnitListQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context.Units.ToListAsync(cancellationToken);
    }
}
