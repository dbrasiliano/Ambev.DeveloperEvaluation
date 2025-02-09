namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

/// <summary>
/// Defines the contract for a sale in the system.
/// </summary>
public interface ISale
{
    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the sale number.
    /// </summary>
    string SaleNumber { get; }

    /// <summary>
    /// Gets the total sale amount.
    /// </summary>
    decimal TotalAmount { get; }

    /// <summary>
    /// Gets the branch where the sale was made.
    /// </summary>
    string BranchId { get; }

    /// <summary>
    /// Gets the customer ID associated with the sale.
    /// </summary>
    string CustomerId { get; }

    /// <summary>
    /// Gets a boolean value indicating whether the sale is cancelled.
    /// </summary>
    bool IsCancelled { get; }

    /// <summary>
    /// Gets the list of items in the sale.
    /// </summary>
    IEnumerable<ISaleItem> Items { get; }

}
