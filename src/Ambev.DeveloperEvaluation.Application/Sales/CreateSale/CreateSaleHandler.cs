using Ambev.DeveloperEvaluation.Application.Sales.Createsale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMessagingService _messagingService;
        private readonly ILogger<CreateSaleHandler> _logger;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMessagingService messagingService, ILogger<CreateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _messagingService = messagingService;
            _logger = logger;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando o processamento do comando CreateSale para a venda {SaleNumber}", command.SaleNumber);

            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validação falhou para a venda {SaleNumber}: {Errors}", command.SaleNumber,
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return new CreateSaleResult
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var sale = new Sale
            {
                SaleNumber = command.SaleNumber,
                BranchId = command.BranchId,
                CustomerId = command.CustomerId,
                CreatedAt = DateTime.UtcNow,
                IsCancelled = command.IsCancelled
            };

            if (command.Items != null)
            {
                foreach (var item in command.Items)
                {
                    sale.AddItem(new SaleItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }
            }

            try
            {
                var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

                _logger.LogInformation("Venda criada com sucesso. SaleId: {SaleId}", createdSale.Id);

                var saleCreatedEvent = new SaleCreatedEvent
                {
                    SaleId = createdSale.Id,
                    SaleNumber = createdSale.SaleNumber,
                    TotalAmount = createdSale.TotalAmount,
                    BranchId = createdSale.BranchId,
                    CustomerId = createdSale.CustomerId,
                    Items = createdSale.Items.Select(item => new SaleItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.UnitPrice
                    }).ToList()
                };

                await _messagingService.SendMessageAsync(saleCreatedEvent);

                _logger.LogInformation("Evento SaleCreatedEvent publicado para a venda {SaleId}", createdSale.Id);

                return new CreateSaleResult
                {
                    Success = true,
                    SaleId = createdSale.Id,
                    TotalAmount = createdSale.TotalAmount,
                    CreatedAt = createdSale.CreatedAt,
                    SaleNumber = createdSale.SaleNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar a venda {SaleNumber}", command.SaleNumber);
                return new CreateSaleResult
                {
                    Success = false,
                    Errors = new List<string> { "Erro ao criar a venda: " + ex.Message }
                };
            }
        }
    }
}
