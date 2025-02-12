namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

/// <summary>
/// Defines the contract for a sale item in the system.
/// </summary>
public interface ISaleItem
{
    /// <summary>
    /// Gets the unique identifier of the sale item.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the sale ID to which this item belongs.
    /// </summary>
    string SaleId { get; }

    /// <summary>
    /// Gets the product ID.
    /// </summary>
    string ProductId { get; }

    /// <summary>
    /// Gets the quantity of the product sold.
    /// </summary>
    int Quantity { get; }

    /// <summary>
    /// Gets the unit price of the product.
    /// </summary>
    decimal UnitPrice { get; }

    /// <summary>
    /// Gets the discount applied to the item.
    /// </summary>
    decimal Discount { get; }

    /// <summary>
    /// Gets the total amount for the item after applying quantity and discount.
    /// </summary>
    decimal TotalItemAmount { get; }
}
