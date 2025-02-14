using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMessagingService _messagingService;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly CreateSaleHandler _handler;
        
        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _messagingService = Substitute.For<IMessagingService>();
            _logger = Substitute.For<ILogger<CreateSaleHandler>>();
            _handler = new CreateSaleHandler(_saleRepository, _mapper, _messagingService, _logger);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = command.SaleNumber,
                BranchId = command.BranchId,
                CustomerId = command.CustomerId,
                Items = command.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            var result = new CreateSaleResult { SaleId = sale.Id };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            createSaleResult.Should().NotBeNull();
            createSaleResult.SaleId.Should().Be(sale.Id);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new CreateSaleCommand();
            var act = await _handler.Handle(command, CancellationToken.None);
            
            act.Should().NotBeNull();
            act.SaleId.Should().BeNull();
            act.Errors.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then sale is saved in repository")]
        public async Task Handle_ValidRequest_SavesSaleToRepository()
        {
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = command.SaleNumber,
                BranchId = command.BranchId,
                CustomerId = command.CustomerId,
                Items = command.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.SaleId.Should().Be(sale.Id);
        }
    }
}
