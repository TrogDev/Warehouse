using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Clients.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private readonly IApplicationDbContext context;

    public CreateClientCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Клиент с таким именем уже существует");
        RuleFor(e => e.Address).NotEmpty();
    }

    private async Task<bool> IsNameUnique(
        CreateClientCommand command,
        string name,
        CancellationToken token
    )
    {
        return !await context.Clients.AnyAsync(e => e.Name == name, token);
    }
}
