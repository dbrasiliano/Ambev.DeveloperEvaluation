using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale item in the system, including item details and validation.
    /// </summary>
    public class SaleItem : BaseEntity, ISaleItem
    {
        /// <summary>
        /// Gets or sets the sale ID to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the sale associated with this sale item.
        /// </summary>
        public Sale Sale { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product ID for this sale item.
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the sale item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets the total amount for this sale item (after applying quantity and discount).
        /// </summary>
        public decimal TotalItemAmount => Quantity * UnitPrice - Discount;


        /// <summary>
        /// Gets the unique identifier of the sale item.
        /// </summary>
        string ISaleItem.Id => Id.ToString();

        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        string ISaleItem.SaleId => SaleId.ToString();

        /// <summary>
        /// Initializes a new instance of the SaleItem class.
        /// </summary>
        public SaleItem()
        {
        }

        /// <summary>
        /// Validates the sale item using the SaleItemValidator rules.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
