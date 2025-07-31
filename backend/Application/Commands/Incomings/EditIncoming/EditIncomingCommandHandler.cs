using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Queries.Balances.GetBalance;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Incomings.EditIncoming;

public class EditIncomingCommandHandler : IRequestHandler<EditIncomingCommand>
{
    private readonly IApplicationDbContext context;
    private readonly ISender mediator;

    public EditIncomingCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        this.context = context;
        this.mediator = mediator;
    }

    public async Task Handle(EditIncomingCommand request, CancellationToken cancellationToken)
    {
        Incoming? incoming = await context
            .Incomings.Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (incoming == null)
        {
            throw new EntityNotFoundException();
        }

        foreach (IncomingItem item in incoming.Items)
        {
            int balance = await mediator.Send(
                new GetBalanceQuery() { ResourceId = item.ResourceId, UnitId = item.UnitId }
            );
            int oldQuantity = item.Quantity;
            int newQuantity =
                request
                    .Items.FirstOrDefault(
                        e => e.ResourceId == item.ResourceId && e.UnitId == item.UnitId
                    )
                    ?.Quantity ?? 0;

            if (balance - oldQuantity + newQuantity < 0)
            {
                throw new NotEnoughResourcesException();
            }
        }

        incoming.Number = request.Number;
        incoming.Date = request.Date;
        incoming.Items = request
            .Items.Select(
                e =>
                    new IncomingItem()
                    {
                        UnitId = e.UnitId,
                        ResourceId = e.ResourceId,
                        Quantity = e.Quantity
                    }
            )
            .ToList();
        await context.SaveChangesAsync(cancellationToken);
    }
}
