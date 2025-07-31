using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Clients.EditClient;

public class UpdateCurrentUserCommandValidator : AbstractValidator<EditClientCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateCurrentUserCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Клиент с таким именем уже существует");
        RuleFor(e => e.Address).NotEmpty();
    }

    private async Task<bool> IsNameUnique(
        EditClientCommand command,
        string name,
        CancellationToken token
    )
    {
        Client? resource = await context.Clients.FirstOrDefaultAsync(e => e.Name == name);

        if (resource == null)
        {
            return true;
        }
        if (resource.Id == command.Id)
        {
            return true;
        }

        return false;
    }
}
