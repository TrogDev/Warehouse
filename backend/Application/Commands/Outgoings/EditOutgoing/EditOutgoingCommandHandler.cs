using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.DTO;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Queries.Balances.GetBalance;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Outgoings.EditOutgoing;

public class EditOutgoingCommandHandler : IRequestHandler<EditOutgoingCommand>
{
    private readonly IApplicationDbContext context;
    private readonly ISender mediator;

    public EditOutgoingCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        this.context = context;
        this.mediator = mediator;
    }

    public async Task Handle(EditOutgoingCommand request, CancellationToken cancellationToken)
    {
        Outgoing? outgoing = await context
            .Outgoings.Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (outgoing == null)
        {
            throw new EntityNotFoundException();
        }

        await ValidateBalance(outgoing, request);

        outgoing.ClientId = request.ClientId;
        outgoing.IsSigned = request.IsSigned;
        outgoing.Number = request.Number;
        outgoing.Date = request.Date;
        outgoing.Items = request
            .Items.Select(
                e =>
                    new OutgoingItem()
                    {
                        UnitId = e.UnitId,
                        ResourceId = e.ResourceId,
                        Quantity = e.Quantity
                    }
            )
            .ToList();

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateBalance(Outgoing outgoing, EditOutgoingCommand request)
    {
        if (!request.IsSigned)
        {
            return;
        }

        foreach (OutgoingItem item in outgoing.Items)
        {
            int balance = await mediator.Send(
                new GetBalanceQuery() { ResourceId = item.ResourceId, UnitId = item.UnitId }
            );
            int oldQuantity = outgoing.IsSigned ? item.Quantity : 0;
            int newQuantity =
                request
                    .Items.FirstOrDefault(
                        e => e.ResourceId == item.ResourceId && e.UnitId == item.UnitId
                    )
                    ?.Quantity ?? 0;

            if (balance + oldQuantity - newQuantity < 0)
            {
                throw new NotEnoughResourcesException();
            }
        }
    }
}
