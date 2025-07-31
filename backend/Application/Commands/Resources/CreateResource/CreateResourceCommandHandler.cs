using MediatR;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Resources.CreateResource;

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand>
{
    private readonly IApplicationDbContext context;

    public CreateResourceCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        Resource resource = new Resource { Name = request.Name, Status = request.Status };
        context.Resources.Add(resource);
        await context.SaveChangesAsync(cancellationToken);
    }
}
