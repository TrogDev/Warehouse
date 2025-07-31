using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Commands.Resources.EditResource;

public class UpdateCurrentUserCommandValidator : AbstractValidator<EditResourceCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateCurrentUserCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Ресурс с таким именем уже существует");
    }

    private async Task<bool> IsNameUnique(
        EditResourceCommand command,
        string name,
        CancellationToken token
    )
    {
        Resource? resource = await context.Resources.FirstOrDefaultAsync(e => e.Name == name);

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
