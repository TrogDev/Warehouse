using MediatR;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Incomings.CreateIncoming;

public class CreateIncomingCommandHandler : IRequestHandler<CreateIncomingCommand>
{
    private readonly IApplicationDbContext context;

    public CreateIncomingCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(CreateIncomingCommand request, CancellationToken cancellationToken)
    {
        Incoming incoming = new Incoming
        {
            Number = request.Number,
            Date = request.Date,
            Items = request
                .Items.Select(
                    e =>
                        new IncomingItem()
                        {
                            ResourceId = e.ResourceId,
                            UnitId = e.UnitId,
                            Quantity = e.Quantity
                        }
                )
                .ToList()
        };
        context.Incomings.Add(incoming);
        await context.SaveChangesAsync(cancellationToken);
    }
}
