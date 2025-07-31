using MediatR;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Units.CreateUnit;

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand>
{
    private readonly IApplicationDbContext context;

    public CreateUnitCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Unit unit = new Domain.Entities.Unit
        {
            Name = request.Name,
            Status = request.Status
        };

        context.Units.Add(unit);
        await context.SaveChangesAsync(cancellationToken);
    }
}
