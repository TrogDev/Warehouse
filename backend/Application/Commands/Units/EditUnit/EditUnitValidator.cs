using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Units.EditUnit;

public class UpdateCurrentUserCommandValidator : AbstractValidator<EditUnitCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateCurrentUserCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Единица измерения с таким именем уже существует");
    }

    private async Task<bool> IsNameUnique(
        EditUnitCommand command,
        string name,
        CancellationToken token
    )
    {
        Unit? unit = await context.Units.FirstOrDefaultAsync(e => e.Name == name);

        if (unit == null)
        {
            return true;
        }
        if (unit.Id == command.Id)
        {
            return true;
        }

        return false;
    }
}
