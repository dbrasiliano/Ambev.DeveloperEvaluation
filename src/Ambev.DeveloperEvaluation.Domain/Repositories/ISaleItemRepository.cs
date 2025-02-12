using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface de repositório para operações relacionadas à entidade SaleItem
/// </summary>
public interface ISaleItemRepository
{
    /// <summary>
    /// Cria um novo item de venda no repositório
    /// </summary>
    /// <param name="saleItem">O item de venda a ser criado</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O item de venda criado</returns>
    Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera um item de venda pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único do item de venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O item de venda se encontrado, null caso contrário</returns>
    Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera todos os itens de venda associados a uma venda específica
    /// </summary>
    /// <param name="saleId">O identificador único da venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Uma lista de itens de venda</returns>
    Task<List<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza um item de venda existente
    /// </summary>
    /// <param name="saleItem">O item de venda a ser atualizado</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O item de venda atualizado</returns>
    Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleta um item de venda do repositório
    /// </summary>
    /// <param name="id">O identificador único do item de venda a ser deletado</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se o item de venda foi deletado, false se não encontrado</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
