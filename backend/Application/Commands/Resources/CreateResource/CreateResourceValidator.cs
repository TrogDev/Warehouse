using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Resources.CreateResource;

public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    private readonly IApplicationDbContext context;

    public CreateResourceCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Name)
            .NotEmpty()
            .MustAsync(IsNameUnique)
            .WithMessage("Ресурс с таким именем уже существует");
    }

    private async Task<bool> IsNameUnique(
        CreateResourceCommand command,
        string name,
        CancellationToken token
    )
    {
        return !await context.Resources.AnyAsync(e => e.Name == name, token);
    }
}
