namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Modelo de resposta da API para a operação de criação de venda.
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// O identificador único da venda criada.
    /// </summary>
    public Guid Id { get; set; }

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
}

/// <summary>
/// Modelo de resposta para um item da venda.
/// </summary>
public class SaleItemResponse
{
    /// <summary>
    /// O identificador do produto.
    /// </summary>
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// A quantidade do produto vendido.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// O preço unitário do produto.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
