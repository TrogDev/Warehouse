using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Units.DeleteUnit;

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteUnitCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Unit? unit = await context.Units.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (unit == null)
        {
            throw new EntityNotFoundException();
        }

        context.Units.Remove(unit);
        await context.SaveChangesAsync(cancellationToken);
    }
}
