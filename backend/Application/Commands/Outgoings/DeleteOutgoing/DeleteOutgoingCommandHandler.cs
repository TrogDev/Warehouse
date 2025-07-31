using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Outgoings.DeleteOutgoing;

public class DeleteOutgoingCommandHandler : IRequestHandler<DeleteOutgoingCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteOutgoingCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteOutgoingCommand request, CancellationToken cancellationToken)
    {
        Outgoing? outgoing = await context.Outgoings.FirstOrDefaultAsync(e => e.Id == request.Id);

        if (outgoing == null)
        {
            throw new EntityNotFoundException();
        }

        context.Outgoings.Remove(outgoing);
        await context.SaveChangesAsync(cancellationToken);
    }
}
