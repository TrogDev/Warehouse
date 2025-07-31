using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Units.EditUnit;

public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand>
{
    private readonly IApplicationDbContext context;

    public EditUnitCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(EditUnitCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Unit? unit = await context.Units.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (unit == null)
        {
            throw new EntityNotFoundException();
        }

        unit.Name = request.Name;
        unit.Status = request.Status;

        await context.SaveChangesAsync(cancellationToken);
    }
}
