using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Resources.GetResource;

public class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, Resource>
{
    private readonly IApplicationDbContext context;

    public GetResourceQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Resource> Handle(
        GetResourceQuery request,
        CancellationToken cancellationToken
    )
    {
        Resource? resource = await context.Resources.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (resource == null)
        {
            throw new EntityNotFoundException();
        }

        return resource;
    }
}
