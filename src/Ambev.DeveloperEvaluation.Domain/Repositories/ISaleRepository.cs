using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface de repositório para operações relacionadas à entidade Sale
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Cria uma nova venda no repositório
    /// </summary>
    /// <param name="sale">A venda a ser criada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A venda criada</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera uma venda pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único da venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A venda se encontrada, null caso contrário</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera todas as vendas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Uma lista de vendas</returns>
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza uma venda existente
    /// </summary>
    /// <param name="sale">A venda a ser atualizada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A venda atualizada</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleta uma venda do repositório
    /// </summary>
    /// <param name="id">O identificador único da venda a ser deletada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se a venda foi deletada, false se não encontrada</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
