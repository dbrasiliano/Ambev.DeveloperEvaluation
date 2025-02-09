using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number cannot be empty.")
                .MaximumLength(20).WithMessage("Sale number cannot be longer than 20 characters.");

            RuleFor(sale => sale.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

            RuleFor(sale => sale.BranchId)
                .NotEmpty().WithMessage("Branch ID cannot be empty.");

            RuleFor(sale => sale.CustomerId)
                .NotEmpty().WithMessage("Customer ID cannot be empty.");

            RuleFor(sale => sale.IsCancelled)
                .Must(isCancelled => !isCancelled).WithMessage("Sale cannot be cancelled at creation.");
        }
    }
}
