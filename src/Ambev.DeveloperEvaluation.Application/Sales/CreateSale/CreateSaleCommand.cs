using Ambev.DeveloperEvaluation.Application.Sales.Createsale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; } = string.Empty;

        public string BranchId { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;

        public bool IsCancelled { get; set; }

        public List<CreateSaleItemCommand>? Items { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }

    public class CreateSaleItemCommand
    {
        public string ProductId { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
