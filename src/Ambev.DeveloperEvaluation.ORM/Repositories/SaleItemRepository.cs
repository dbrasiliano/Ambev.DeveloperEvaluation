using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repositório para operações relacionadas à entidade SaleItem.
/// </summary>
public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="SaleItemRepository"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria um novo item de venda no repositório.
    /// </summary>
    /// <param name="saleItem">O item de venda a ser criado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O item de venda criado.</returns>
    public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        await _context.SaleItems.AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    /// <summary>
    /// Recupera um item de venda pelo seu identificador único.
    /// </summary>
    /// <param name="id">O identificador único do item de venda.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O item de venda se encontrado, null caso contrário.</returns>
    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <summary>
    /// Recupera todos os itens de venda associados a uma venda específica.
    /// </summary>
    /// <param name="saleId">O identificador único da venda.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma lista de itens de venda.</returns>
    public async Task<List<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .Where(si => si.SaleId == saleId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Atualiza um item de venda existente.
    /// </summary>
    /// <param name="saleItem">O item de venda a ser atualizado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O item de venda atualizado.</returns>
    public async Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        _context.SaleItems.Update(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    /// <summary>
    /// Deleta um item de venda do repositório.
    /// </summary>
    /// <param name="id">O identificador único do item de venda a ser deletado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>True se o item de venda foi deletado, false se não encontrado.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var saleItem = await _context.SaleItems.FindAsync(new object[] { id }, cancellationToken);
        if (saleItem == null)
            return false;

        _context.SaleItems.Remove(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
