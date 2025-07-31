using FluentValidation;

namespace Warehouse.Application.Commands.Incomings.EditIncoming;

public class EditIncomingValidator : AbstractValidator<EditIncomingCommand>
{
    public EditIncomingValidator()
    {
        RuleFor(e => e.Number).NotEmpty();
        RuleFor(e => e.Date).NotEmpty();
        RuleFor(e => e.Items)
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("Количество каждого товара должно быть больше нуля");
    }
}
