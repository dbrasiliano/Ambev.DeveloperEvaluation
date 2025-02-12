using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Createsale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("O número da venda é obrigatório.")
                .MaximumLength(50).WithMessage("O número da venda deve ter no máximo 50 caracteres.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("A quantidade do item deve ser maior que zero.");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("O preço unitário do item deve ser maior que zero.");
        });
    }
}