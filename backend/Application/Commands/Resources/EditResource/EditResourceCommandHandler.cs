using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Resources.EditResource;

public class EditResourceCommandHandler : IRequestHandler<EditResourceCommand>
{
    private readonly IApplicationDbContext context;

    public EditResourceCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(EditResourceCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Resource? resource = await context.Resources.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (resource == null)
        {
            throw new EntityNotFoundException();
        }

        resource.Name = request.Name;
        resource.Status = request.Status;

        await context.SaveChangesAsync(cancellationToken);
    }
}
