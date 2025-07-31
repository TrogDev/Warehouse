using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Clients.EditClient;

public class EditClientCommandHandler : IRequestHandler<EditClientCommand>
{
    private readonly IApplicationDbContext context;

    public EditClientCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(EditClientCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Client? client = await context.Clients.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (client == null)
        {
            throw new EntityNotFoundException();
        }

        client.Name = request.Name;
        client.Address = request.Address;
        client.Status = request.Status;

        await context.SaveChangesAsync(cancellationToken);
    }
}
