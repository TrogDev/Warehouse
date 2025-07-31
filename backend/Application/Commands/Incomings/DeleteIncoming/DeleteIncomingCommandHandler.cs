using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Queries.Balances.GetBalance;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Incomings.DeleteIncoming;

public class DeleteIncomingCommandHandler : IRequestHandler<DeleteIncomingCommand>
{
    private readonly IApplicationDbContext context;
    private readonly ISender mediator;

    public DeleteIncomingCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        this.context = context;
        this.mediator = mediator;
    }

    public async Task Handle(DeleteIncomingCommand request, CancellationToken cancellationToken)
    {
        Incoming? incoming = await context
            .Incomings.Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        if (incoming == null)
        {
            throw new EntityNotFoundException();
        }

        await ValidateBalance(incoming);

        context.Incomings.Remove(incoming);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateBalance(Incoming incoming)
    {
        foreach (IncomingItem item in incoming.Items)
        {
            int balance = await mediator.Send(
                new GetBalanceQuery() { ResourceId = item.ResourceId, UnitId = item.UnitId }
            );
            Console.WriteLine("balance: " + balance.ToString());
            if (balance < item.Quantity)
            {
                throw new NotEnoughResourcesException();
            }
        }
    }
}
