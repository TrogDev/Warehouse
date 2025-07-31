using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Units.CreateUnit;

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    private readonly IApplicationDbContext context;

    public CreateUnitCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Единица измерения с таким именем уже существует");
    }

    private async Task<bool> IsNameUnique(
        CreateUnitCommand command,
        string name,
        CancellationToken token
    )
    {
        return !await context.Units.AnyAsync(e => e.Name == name, token);
    }
}
