namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Representa uma requisição para criar uma nova venda no sistema.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Obtém ou define o número da venda.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o valor total da venda.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Obtém ou define o identificador da filial onde a venda foi realizada.
    /// </summary>
    public string BranchId { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o identificador do cliente associado à venda.
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define uma lista de itens vendidos.
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Representa um item na requisição de criação de venda.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Obtém ou define o identificador do produto.
    /// </summary>
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define a quantidade do produto vendido.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Obtém ou define o preço unitário do produto.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
