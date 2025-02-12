using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.SaleId)
                .NotEmpty().WithMessage("Sale ID cannot be empty.");

            RuleFor(item => item.ProductId)
                .NotEmpty().WithMessage("Product ID cannot be empty.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");

            RuleFor(item => item.TotalItemAmount)
                .GreaterThan(0).WithMessage("Total item amount must be greater than zero.");
        }
    }
}
