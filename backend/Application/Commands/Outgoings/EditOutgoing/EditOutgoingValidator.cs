using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Interfaces;

namespace Warehouse.Application.Commands.Outgoings.EditOutgoing;

public class EditOutgoingValidator : AbstractValidator<EditOutgoingCommand>
{
    private readonly IApplicationDbContext context;

    public EditOutgoingValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.ClientId)
            .NotEmpty()
            .MustAsync(IsClientExists)
            .WithMessage("Клиент с указанным идентификатором не найден");
        RuleFor(e => e.Number).NotEmpty();
        RuleFor(e => e.Date).NotEmpty();
        RuleFor(e => e.Items)
            .NotEmpty()
            .WithMessage("Список товаров не может быть пустым")
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("Количество каждого товара должно быть больше нуля");
    }

    private async Task<bool> IsClientExists(long clientId, CancellationToken token)
    {
        return await context.Clients.AnyAsync(e => e.Id == clientId, token);
    }
}
