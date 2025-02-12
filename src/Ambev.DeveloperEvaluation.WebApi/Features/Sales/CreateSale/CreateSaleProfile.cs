using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities; // Adicione esse namespace

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Perfil para mapeamento entre as classes da Aplicação e da API para a funcionalidade de criação de venda.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
            CreateMap<CreateSaleResult, CreateSaleResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SaleId));
            CreateMap<SaleItem, SaleItemResponse>();
        }
    }
}
