namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the result of a sale creation operation.
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the sale was created successfully.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the newly created sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created sale in the system.</value>
    public Guid? SaleId { get; set; }

    /// <summary>
    /// O número da venda.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// O valor total da venda.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// A data e hora em que a venda foi criada.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets a list of errors in case the creation fails.
    /// </summary>
    public List<string>? Errors { get; set; }
}
