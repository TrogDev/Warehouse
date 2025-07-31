using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Clients.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        Client? client = await context.Clients.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (client == null)
        {
            throw new EntityNotFoundException();
        }

        context.Clients.Remove(client);
        await context.SaveChangesAsync(cancellationToken);
    }
}
