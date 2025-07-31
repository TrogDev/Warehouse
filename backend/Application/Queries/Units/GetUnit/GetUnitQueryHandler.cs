using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Queries.Units.GetUnit;

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, Domain.Entities.Unit>
{
    private readonly IApplicationDbContext context;

    public GetUnitQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Domain.Entities.Unit> Handle(
        GetUnitQuery request,
        CancellationToken cancellationToken
    )
    {
        Domain.Entities.Unit? unit = await context.Units.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (unit == null)
        {
            throw new EntityNotFoundException();
        }

        return unit;
    }
}
