using MediatR;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Clients.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IApplicationDbContext context;

    public CreateClientCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = new Client
        {
            Name = request.Name,
            Address = request.Address,
            Status = request.Status
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync(cancellationToken);
    }
}
