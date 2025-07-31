using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Resources.DeleteResource;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteResourceCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        Resource? resource = await context.Resources.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (resource == null)
        {
            throw new EntityNotFoundException();
        }

        context.Resources.Remove(resource);
        await context.SaveChangesAsync(cancellationToken);
    }
}
