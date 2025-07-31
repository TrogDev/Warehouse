using MediatR;
using Warehouse.Application.DTO;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Queries.Balances.GetBalance;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Outgoings.CreateOutgoing;

public class CreateOutgoingCommandHandler : IRequestHandler<CreateOutgoingCommand>
{
    private readonly IApplicationDbContext context;
    private readonly ISender mediator;

    public CreateOutgoingCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        this.context = context;
        this.mediator = mediator;
    }

    public async Task Handle(CreateOutgoingCommand request, CancellationToken cancellationToken)
    {
        await ValidateBalance(request);
        Outgoing outgoing = new Outgoing
        {
            Number = request.Number,
            Date = request.Date,
            ClientId = request.ClientId,
            IsSigned = request.IsSigned,
            Items = request
                .Items.Select(
                    e =>
                        new OutgoingItem()
                        {
                            ResourceId = e.ResourceId,
                            UnitId = e.UnitId,
                            Quantity = e.Quantity
                        }
                )
                .ToList()
        };
        context.Outgoings.Add(outgoing);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateBalance(CreateOutgoingCommand request)
    {
        if (!request.IsSigned)
        {
            return;
        }

        foreach (CreateOrderItemData item in request.Items)
        {
            int balance = await mediator.Send(
                new GetBalanceQuery() { ResourceId = item.ResourceId, UnitId = item.UnitId }
            );
            if (balance < item.Quantity)
            {
                throw new NotEnoughResourcesException();
            }
        }
    }
}
