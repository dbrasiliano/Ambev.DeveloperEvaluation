using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Repositório para operações relacionadas à entidade Sale.
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="SaleRepository"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria uma nova venda no repositório.
    /// </summary>
    /// <param name="sale">A venda a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A venda criada.</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Recupera uma venda pelo seu identificador único.
    /// </summary>
    /// <param name="id">O identificador único da venda.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A venda se encontrada; caso contrário, null.</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Recupera todas as vendas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma lista de vendas.</returns>
    public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Atualiza uma venda existente.
    /// </summary>
    /// <param name="sale">A venda a ser atualizada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A venda atualizada.</returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Deleta uma venda do repositório.
    /// </summary>
    /// <param name="id">O identificador único da venda a ser deletada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>True se a venda foi deletada; caso contrário, false.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FindAsync(new object[] { id }, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
