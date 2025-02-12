using Ambev.DeveloperEvaluation.Application.Sales.Createsale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public string BranchId { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;

        public bool IsCancelled { get; set; }

        public List<SaleItem>? Items { get; set; }

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
}
